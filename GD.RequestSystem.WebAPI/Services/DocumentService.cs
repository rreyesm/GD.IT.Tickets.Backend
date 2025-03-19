using GD.QualityAssurance.Entities.AuthModels;
using GD.QualityAssurance.Entities.Shared;
using GD.RequestSystem.DAL.EF;
using Microsoft.Extensions.Options;

namespace GD.RequestSystem.WebAPI.Services
{
    public interface IDocumentService
    {
        Task<ResultModel<string>> SaveDocument(IFormFile file);
    }
    public class DocumentService : IDocumentService
    {
        private readonly AppSettings _appSettings;
        private readonly RequestSystemDbContext _context;
        public DocumentService(RequestSystemDbContext context, IOptions<AppSettings> options)
        {
            _context = context;
            _appSettings = options.Value;
        }

        public Task<ResultModel<string>> SaveDocument(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
