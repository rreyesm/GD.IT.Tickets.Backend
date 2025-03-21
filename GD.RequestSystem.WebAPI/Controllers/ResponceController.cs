using System.Security.Claims;
using GD.RequestSystem.Entities.Shared;
using GD.RequestSystem.Entities;
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
    public class ResponceController : ControllerBase
    {
        IResponseService responseService;
        public ResponceController(IResponseService rService)
        {
            responseService = rService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateResponsePetition([FromBody] ResultPetition request)
        {
            try
            {
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (request == null)
                {
                    return BadRequest(new ResultModel<string>
                    {
                        Response = false,
                        Data = null,
                        Message = "La no puede ser vacia"
                    });
                }

                var result = await responseService.CreateResponsePetition(request, userID);
                if (!result.Response)
                {

                    return new ObjectResult(new ResultModel<string>
                    {
                        Response = false,
                        Data = null,
                        Message = result.Message
                    });
                }
                return Ok(new ResultModel<ResultPetition>
                {
                    Response = true,
                    Message = result.Message,
                    Data = result.Data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultModel<string>
                {
                    Response = false,
                    Data = null,
                    Message = "Token invalido"
                });
            }
        }
    }
}
