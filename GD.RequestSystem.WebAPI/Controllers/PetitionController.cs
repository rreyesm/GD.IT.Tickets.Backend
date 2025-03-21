using GD.QualityAssurance.Entities.Shared;
using GD.RequestSystem.Entities;
using System.Security.Claims;
using GD.RequestSystem.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using GD.RequestSystem.Entities.RequestModels;

namespace GD.RequestSystem.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AllowSpecificOrigin")]
    public class PetitionController : ControllerBase
    {
        IPetitionService petitionService;
        public PetitionController(IPetitionService pService)
        {
            petitionService = pService;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePetition([FromBody] Petition request)
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
                        Message = "La solicitud no puede estar vacía."
                    });
                }

                var result = await petitionService.CreatePetition(request, userID);
                if (!result.Response)
                {

                    return new ObjectResult(new ResultModel<string>
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
                    Message = ex.Message
                });
            }

        }
        [HttpGet]
        public async Task<ResultModel<List<PetitionResponse>>> GetAllPetitions([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await petitionService.GetAllPetitions(page, pageSize);
                if (result.Response == false)
                {
                    return new ResultModel<List<PetitionResponse>>
                    {
                        Response = false,
                        Data = null,
                        Message = result.Message
                    };
                }
                else
                {
                    return new ResultModel<List<PetitionResponse>>
                    {
                        Response = true,
                        Data = result.Data,
                        Message = result.Message
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<List<PetitionResponse>>
                {
                    Response = false,
                    Data = null,
                    Message = "Error encontrado en la consulta en:" + ex.Message
                };
            }

        }
        [HttpGet("{petitionID}")]
        public async Task<ResultModel<PetitionResponse>> GetPetitionbyID(int petitionID)
        {
            try
            {
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await petitionService.GetPetitionbyID(petitionID);
                if (result.Response == false)
                {
                    return new ResultModel<PetitionResponse>
                    {
                        Response = false,
                        Data = null,
                        Message = result.Message
                    };
                }
                else
                {
                    return new ResultModel<PetitionResponse>
                    {
                        Response = true,
                        Data = result.Data,
                        Message = result.Message
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<PetitionResponse>
                {
                    Response = false,
                    Data = null,
                    Message = "Error encontrado en la consulta en:" + ex.Message
                };
            }
        }
        [HttpGet("{statusID}")]
        public async Task<ResultModel<List<PetitionResponse>>> GetAllPetitionByStatus(int statusID, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await petitionService.GetAllPetitionByStatus(statusID, page, pageSize);
                if (result.Response == false)
                {
                    return new ResultModel<List<PetitionResponse>>
                    {
                        Response = false,
                        Data = null,
                        Message = result.Message
                    };
                }
                else
                {
                    return new ResultModel<List<PetitionResponse>>
                    {
                        Response = true,
                        Data = result.Data,
                        Message = result.Message
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<List<PetitionResponse>>
                {
                    Response = false,
                    Data = null,
                    Message = "Error encontrado en la consulta en:" + ex.Message
                };
            }
        }
        [HttpGet("{AreaID}")]
        public async Task<ResultModel<List<PetitionResponse>>> GetAllPetitionByArea(int AreaID, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await petitionService.GetAllPetitionByArea(AreaID, page, pageSize);
                if (result.Response == false)
                {
                    return new ResultModel<List<PetitionResponse>>
                    {
                        Response = false,
                        Data = null,
                        Message = result.Message
                    };
                }
                else
                {
                    return new ResultModel<List<PetitionResponse>>
                    {
                        Response = true,
                        Data = result.Data,
                        Message = result.Message
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<List<PetitionResponse>>
                {
                    Response = false,
                    Data = null,
                    Message = "Error encontrado en la consulta en:" + ex.Message
                };
            }
        }
        [HttpGet]
        public async Task<ResultModel<List<PetitionResponse>>> GetAllPetitionByUserID([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await petitionService.GetAllPetitionByUserID(userID,page,pageSize);
                if (result.Response == false)
                {
                    return new ResultModel<List<PetitionResponse>>
                    {
                        Response = false,
                        Data = null,
                        Message = result.Message
                    };
                }
                else
                {
                    return new ResultModel<List<PetitionResponse>>
                    {
                        Response = true,
                        Data = result.Data,
                        Message = result.Message
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultModel<List<PetitionResponse>>
                {
                    Response = false,
                    Data = null,
                    Message = "Error encontrado en la consulta en:" + ex.Message
                };
            }
        }
    }
}
