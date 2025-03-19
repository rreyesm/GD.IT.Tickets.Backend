using GD.QualityAssurance.Entities.AuthModels;
using GD.QualityAssurance.Entities.Shared;
using GD.RequestSystem.DAL.EF;
using GD.RequestSystem.Entities;
using GD.RequestSystem.Entities.Catalogs;
using Microsoft.Extensions.Options;

namespace GD.RequestSystem.WebAPI.Services
{
    public interface IFilterService
    {
        Task<IList<string>> GetAreas();
        Task<ResultModel<List<Area>>> GetAllAreas();

        Task<IList<string>> GetStatus();
        Task<ResultModel<List<PetitionStatus>>> GetAllStatus();
       
    }
    public class FilterService : IFilterService
    {
        private readonly AppSettings _appSettings;
        private readonly RequestSystemDbContext _context;
        public FilterService(RequestSystemDbContext context, IOptions<AppSettings> options)
        {
            _context = context;
            _appSettings = options.Value;
        }
        public Task<IList<string>> GetAreas()
        {
            throw new NotImplementedException();
        }
        public Task<ResultModel<List<Area>>> GetAllAreas()
        {
            throw new NotImplementedException();
        }
        public Task<IList<string>> GetStatus()
        {
            throw new NotImplementedException();
        }
        public Task<ResultModel<List<PetitionStatus>>> GetAllStatus()
        {
            throw new NotImplementedException();
        }

    }
}
