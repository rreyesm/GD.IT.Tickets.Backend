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
    public class StatusController : ControllerBase
    {
        IStatusService statusService;
        public StatusController(IStatusService sService)
        {
            statusService = sService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ResultModel<string>> validateStatus(int idStatus)
        {
            return await statusService.validateStatus(idStatus);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] PetitionStatus request)
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
                        Message = "El estatus no puede estar vacio."
                    });
                }
                var result = await statusService.CreateStatus(request, userID);
                if (!result.Response)
                {
                    return new ObjectResult(new ResultModel<string>
                    {
                        Response = false,
                        Data = null,
                        Message = result.Message
                    });
                }

                return Ok(new ResultModel<PetitionStatus>
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
        [HttpPost]
        public async Task<IActionResult> UpdateStatus([FromBody] PetitionStatus request)
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
                        Message = "El estatus no puede estar vacio."
                    });
                }
                var result = await statusService.UpdateStatus(request, userID);
                if (!result.Response)
                {
                    return new ObjectResult(new ResultModel<string>
                    {
                        Response = false,
                        Data = null,
                        Message = result.Message
                    });
                }

                return Ok(new ResultModel<PetitionStatus>
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
        [HttpPut]
        public async Task<IActionResult> UpdateStatusPetititon(int idStatus, int PetitionID)
        {
            try
            {
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (idStatus == 0 || PetitionID ==0)
                {

                    return BadRequest(new ResultModel<string>
                    {
                        Response = false,
                        Data = null,
                        Message = "No se puedo cambiar el estatus de la peticion"
                    });
                }
                var result = await statusService.UpdateStatusPetititon(idStatus, PetitionID, userID);
                if (!result.Response)
                {
                    return new ObjectResult(new ResultModel<Petition>
                    {
                        Response = false,
                        Data = null,
                        Message = result.Message
                    });
                }

                return Ok(new ResultModel<Petition>
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
