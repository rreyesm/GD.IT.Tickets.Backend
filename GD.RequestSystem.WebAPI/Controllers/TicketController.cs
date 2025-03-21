using System.Diagnostics;
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

    public class TicketController : ControllerBase
    {
        ITicketService ticketService;
        public TicketController(ITicketService tService)
        {
            ticketService = tService;
        }
        

    }

}
