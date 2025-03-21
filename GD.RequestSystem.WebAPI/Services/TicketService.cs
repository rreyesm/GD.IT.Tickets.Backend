using System.Data;
using System.Diagnostics;
using GD.RequestSystem.DAL.EF;
using GD.RequestSystem.Entities.AuthModels;
using GD.RequestSystem.Entities.ModelsAdmin;
using GD.RequestSystem.Entities.Shared;
using GD.RequestSystem.DAL.EF;
using GD.RequestSystem.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GD.RequestSystem.WebAPI.Services
{
    public interface ITicketService
    {
    }

    public class TicketService : ITicketService
    {
        private readonly AppSettings _appSettings;
        private readonly RequestSystemDbContext _context;
        public TicketService(RequestSystemDbContext context, IOptions<AppSettings> options)
        {
            _context = context;
            _appSettings = options.Value;
        }
        
    }
}
