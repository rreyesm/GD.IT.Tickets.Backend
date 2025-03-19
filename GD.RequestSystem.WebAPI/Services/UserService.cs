using GD.QualityAssurance.DAL.EF;
using GD.QualityAssurance.Entities.AuthModels;
using GD.QualityAssurance.Entities.ModelsAdmin;
using GD.QualityAssurance.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace GD.QualityAssurance.WebAPI.Services
{
    public interface IUserService
    {
        Task<ResultModel<Account>> Authenticate(LoginModel login);
        Task<IList<string>> GetRoles(int accountID, int ProyectID);
    }
    public class UserService : IUserService
    {
        private readonly GeneralDbContext _context;
        private readonly AppSettings _appSettings;
        private static readonly int ProjectID = 13;

        public UserService(GeneralDbContext context, IOptions<AppSettings> options)
        {
            _context = context;
            _appSettings = options.Value;
        }
        public async Task<ResultModel<Account>> Authenticate(LoginModel login)
        {
            using var connection = _context.Database.GetDbConnection();
            var query = @"SELECT a.*, u.*, d.*
                FROM tblAccounts a
                INNER JOIN tblUsers u ON a.UserID = u.UserID
                LEFT JOIN tblDepartments d ON u.DepartmentID = d.DepartmentID
                WHERE u.NickName = @UserName AND a.ProjectID = @ProjectID ";

            var accountResult = await connection.QueryAsync<Account, User, Department, Account>(
              query, (account, user, Department) =>
              {
                  account.User = user;
                  account.Departament = Department;

                  return account;
              },
              new { UserName = login.UserName, ProjectID = ProjectID },
              splitOn: "UserID,DepartmentID"
              );
            var account = accountResult.FirstOrDefault();
            if (account == null)
            {
                return new ResultModel<Account>
                {
                    Response = false,
                    Data = null,
                    Message = "Esta cuenta no existe"

                };
            }
            if (account.User.NickName != login.UserName)
            {
                return new ResultModel<Account>
                {
                    Response = false,
                    Data = null,
                    Message = "Usuario incorrecto"
                };
            }
            if (account.User.Password != account.User.PasswordControl(login.Password))
            {
                return new ResultModel<Account>
                {
                    Response = false,
                    Data = null,
                    Message = "Contraseña incorrecta"
                };
            }
            if (account.User.IsActive == false)
            {
                return new ResultModel<Account>
                {
                    Response = false,
                    Data = null,
                    Message = "Usuario inactivo"
                };
            }

            IList<string> roles = await GetRoles(account.AccountID, account.ProjectID);

            var userToken = GenerateJwtToken(account, roles);
            account.User.Token = userToken.Token;
            account.User.Expiration = userToken.Expiration;
            account.User.Roles = roles;

            var selectPermission = new HashSet<string> { "Add", "Edit", "Show", "Delete", "All" };
            if (roles.Count > 0)
            {
                string primerElemento = roles[0];
                foreach (var permiso in selectPermission)
                {
                    if (primerElemento.EndsWith(permiso))
                    {
                        account.User.RolesString = primerElemento.Substring(0, primerElemento.Length - permiso.Length);
                        break;
                    }
                }
            }
            return new ResultModel<Account>
            {
                Response = true,
                Data = account,
                Message = "Usuario autenticado"
            };

        }

        public async Task<IList<string>> GetRoles(int accountID, int ProyectID)
        {
            using var connection = _context.Database.GetDbConnection();
            var profileQuery = @"
            SELECT ProfileID 
            FROM tblAccounts 
            WHERE AccountID = @AccountID";

            var profile = await connection.QueryFirstOrDefaultAsync<int?>(
                profileQuery,
                new { AccountID = accountID }
            );

            if (profile == null)
            {
                return new List<string>();
            }
            var permissionsQuery = @"
            SELECT 
                p.ModuleID,
                p.ModuleName,
                p.Show, 
                p.[Add], 
                p.Edit, 
                p.[Delete], 
                p.[All] 
            FROM tblPermissions p
            INNER JOIN tblProfiles pr ON p.ProfileID = pr.ProfileID
            WHERE p.ProfileID = @ProfileID AND pr.ProjectID = @ProjectID";

            var sysPermissions = await connection.QueryAsync<Permission>(
                permissionsQuery,
                new { ProfileID = profile, ProjectID = ProyectID }
            );

            if (!sysPermissions.Any())
            {
                return new List<string>();
            }
            var moduleIds = sysPermissions.Select(x => x.ModuleID).ToList();
            var modulesQuery = @"
            SELECT 
                ModuleID, 
                Name 
            FROM tblModules 
            WHERE IsActive = 1 AND ModuleID IN @ModuleIDs AND ProjectID = @ProjectID";

            var sysModules = await connection.QueryAsync<Module>(
                modulesQuery,
                new { ModuleIDs = moduleIds, ProjectID = ProyectID }
            );
            var res = from p in sysPermissions
                      join m in sysModules on p.ModuleID equals m.ModuleID
                      select new
                      {
                          p.Show,
                          p.Add,
                          p.Edit,
                          p.Delete,
                          p.All,
                          m.Name
                      };
            // Construir la lista de roles
            IList<string> roles = new List<string>();
            foreach (var item in res)
            {
                if (item.Show) roles.Add(item.Name + "Show");
                if (item.Add) roles.Add(item.Name + "Add");
                if (item.Edit) roles.Add(item.Name + "Edit");
                if (item.Delete) roles.Add(item.Name + "Delete");
                if (item.All) roles.Add(item.Name + "All");
            }

            return roles;
        }
        private UserToken GenerateJwtToken(Account account, IList<string> roles)
        {
            var claims = new List<Claim>
        {
        new Claim(JwtRegisteredClaimNames.UniqueName, account.User.NickName),
        new Claim(ClaimTypes.Name, account.User.NickName),
        new Claim(ClaimTypes.NameIdentifier, account.User.UserID.ToString()),
        new Claim(ClaimTypes.GivenName, account.User.GetName),
        new Claim(ClaimTypes.Actor, account.User.DepartmentID != 0 ? account.User.DepartmentID.ToString() : "0"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())};

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(24);
            var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: creds);

            return new UserToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
