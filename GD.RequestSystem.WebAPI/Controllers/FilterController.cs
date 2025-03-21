using System.Security.Claims;
using GD.RequestSystem.Entities.ModelsAdmin;
using GD.RequestSystem.Entities.Shared;
using GD.RequestSystem.Entities;
using GD.RequestSystem.Entities.Catalogs;
using GD.RequestSystem.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GD.RequestSystem.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AllowSpecificOrigin")]
    public class FilterController : ControllerBase
    {
        IFilterService filterService;
        public FilterController(IFilterService fService)
        {
            filterService = fService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IList<string>> GetAreas()
        {
            return await filterService.GetAreas();
        }
        [HttpGet]
        public async Task<ResultModel<List<Area>>> GetAllAreas()
        {
            try
            {
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await filterService.GetAllAreas();
                if (result.Response == false)
                {
                    return new ResultModel<List<Area>>
                    {
                        Response = false,
                        Data = null,
                        Message = result.Message
                    };
                }
                else
                {
                    return new ResultModel<List<Area>>
                    {
                        Response = true,
                        Data = result.Data,
                        Message = result.Message
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<List<Area>>
                {
                    Response = false,
                    Data = null,
                    Message = ex.Message
                };
            }

        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IList<string>> GetStatus()
        {
            return await filterService.GetStatus();
        }
        [HttpGet]
        public async Task<ResultModel<List<PetitionStatus>>> GetAllStatus()
        {
            try
            {
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await filterService.GetAllStatus();
                if (result.Response == false)
                {
                    return new ResultModel<List<PetitionStatus>>
                    {
                        Response = false,
                        Data = null,
                        Message = result.Message
                    };
                }
                else
                {
                    return new ResultModel<List<PetitionStatus>>
                    {
                        Response = true,
                        Data = result.Data,
                        Message = result.Message
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<List<PetitionStatus>>
                {
                    Response = false,
                    Data = null,
                    Message = ex.Message
                };
            }

        }
       
    }
}
