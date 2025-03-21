using GD.RequestSystem.DAL.EF;
using GD.RequestSystem.Entities.AuthModels;
using GD.RequestSystem.Entities.ModelsAdmin;
using GD.RequestSystem.Entities.Shared;
using GD.RequestSystem.DAL.EF;
using Microsoft.Extensions.Options;

namespace GD.RequestSystem.WebAPI.Services
{
    public interface IRolesService
    {
        Task<IList<string>> GetUserDevelopers();
        Task<ResultModel<List<User>>> GetAllUserDevelopers();
        Task<IList<string>> GetUserEmployments();
        Task<ResultModel<List<User>>> GetAllUserEmployments();
    }
    public class RolesService : IRolesService
    {
        private readonly AppSettings _appSettings;
        private readonly GeneralDbContext _context;
        public RolesService(GeneralDbContext context, IOptions<AppSettings> options)
        {
            _context = context;
            _appSettings = options.Value;
        }

        public Task<ResultModel<List<User>>>GetAllUserDevelopers()
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<List<User>>> GetAllUserEmployments()
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetUserDevelopers()
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetUserEmployments()
        {
            throw new NotImplementedException();
        }
    }
}
