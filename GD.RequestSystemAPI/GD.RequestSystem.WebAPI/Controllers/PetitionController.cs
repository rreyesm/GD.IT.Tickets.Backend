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
            catch
            {
                return BadRequest(new ResultModel<string>
                {
                    Response = false,
                    Data = null,
                    Message = "Token invalido"
                });
            }

        }
        [HttpGet]
        public async Task<ResultModel<List<PetitionResponse>>> GetAllPetitions()
        {
            try
            {
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await petitionService.GetAllPetitions();
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
        public async Task<ResultModel<List<PetitionResponse>>> GetAllPetitionByStatus(int statusID)
        {
            try
            {
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await petitionService.GetAllPetitionByStatus(statusID);
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
        public async Task<ResultModel<List<PetitionResponse>>> GetAllPetitionByArea(int AreaID)
        {
            try
            {
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await petitionService.GetAllPetitionByArea(AreaID);
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
        public async Task<ResultModel<List<PetitionResponse>>> GetAllPetitionByUserID()
        {
            try
            {
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var result = await petitionService.GetAllPetitionByUserID(userID);
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
