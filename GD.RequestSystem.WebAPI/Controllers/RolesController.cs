using GD.RequestSystem.Entities.Shared;
using GD.RequestSystem.Entities;
using System.Security.Claims;
using GD.RequestSystem.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using GD.RequestSystem.Entities.ModelsAdmin;

namespace GD.RequestSystem.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AllowSpecificOrigin")]
    public class RolesController : ControllerBase
    {
        IRolesService rolesService;
        public RolesController(IRolesService rService)
        {
            rolesService = rService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IList<string>> GetUserDevelopers()
        {
            return await rolesService.GetUserDevelopers();
        }
        [HttpGet]
        public async Task<ResultModel<List<User>>> GetAllUserDevelopers()
        {
            try
            {
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await rolesService.GetAllUserDevelopers();
                if (result.Response == false)
                {
                    return new ResultModel<List<User>>
                    {
                        Response = false,
                        Data = null,
                        Message = result.Message
                    };
                }
                else
                {
                    return new ResultModel<List<User>>
                    {
                        Response = true,
                        Data = result.Data,
                        Message = result.Message
                    };
                }
            }
            catch(Exception ex)
            {
                return new ResultModel<List<User>>
                {
                    Response = false,
                    Data = null,
                    Message = ex.Message
                };
            }
            
        }
    }
}
