using GD.QualityAssurance.Entities.AuthModels;
using GD.QualityAssurance.Entities.ModelsAdmin;
using GD.QualityAssurance.Entities.Shared;
using GD.QualityAssurance.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GD.QualityAssurance.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class UserController : ControllerBase
    {
        IUserService userService;
        public UserController(IUserService uService)
        {
            userService = uService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel request)
        {
            var result = await userService.Authenticate(request);

            if (!result.Response)
                return new ObjectResult(new ResultModel<string>
                {
                    Response = false,
                    Data = null,
                    Message = result.Message
                });

            var userToken = new UserToken
            {
                UserID = result.Data.UserID,
                Name = result.Data.User.Name,
                DepartmentID = result.Data.Departament.DepartmentID - 1,
                Roles = result.Data.User.Roles,
                roll = result.Data.User.RolesString,
                Department = result.Data.Departament.Name,
                Token = result.Data.User.Token,
                Expiration = result.Data.User.Expiration,
            };

            return Ok(new ResultModel<UserToken>
            {
                Response = true,
                Message = result.Message,
                Data = userToken
            });

        }
        [HttpOptions]
        public IActionResult Preflight()
        {
            return NoContent();
        }
    }

}
