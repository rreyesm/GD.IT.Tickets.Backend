using System.Security.Claims;
using GD.QualityAssurance.Entities.Shared;
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
    public class DocumentController : ControllerBase
    {
        IDocumentService documentService;
        public DocumentController(IDocumentService dService)
        {
            documentService = dService;
        }
        [HttpPost]
        public async Task<IActionResult> SaveDocument([FromBody] IFormFile file)
        {
            if(file == null)
            {
                return BadRequest(new ResultModel<string>
                {
                    Response = false,
                    Data = null,
                    Message = "No se ha enviado un archivo."
                });
            }
            var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await documentService.SaveDocument(file);
            if (!result.Response)
            {
                return new ObjectResult(new ResultModel<string>
                {
                    Response = false,
                    Data = null,
                    Message = result.Message
                });
            }
            return Ok(new ResultModel<string>
            {
                Response = true,
                Message = result.Message,
                Data = "Archivo guardado correctamente."
            });
        }
    }
}
