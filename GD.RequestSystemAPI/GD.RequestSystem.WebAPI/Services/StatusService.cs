using GD.QualityAssurance.Entities.AuthModels;
using GD.QualityAssurance.Entities.Shared;
using GD.RequestSystem.DAL.EF;
using GD.RequestSystem.Entities;
using Microsoft.Extensions.Options;

namespace GD.RequestSystem.WebAPI.Services
{
    public interface IStatusService
    {
        Task<ResultModel<PetitionStatus>> CreateStatus(PetitionStatus petitionStatis, int userID);
        Task<ResultModel<PetitionStatus>> UpdateStatus(PetitionStatus petitionStatis, int userID);
        Task<ResultModel<Petition>> UpdateStatusPetititon(int idStatus, int PetitionID, int userID);
        Task<ResultModel<string>> validateStatus(int idStatus);

    }
    public class StatusService : IStatusService
    {
        private readonly AppSettings _appSettings;
        private readonly RequestSystemDbContext _context;
        public StatusService(RequestSystemDbContext context, IOptions<AppSettings> options)
        {
            _context = context;
            _appSettings = options.Value;
        }

        public Task<ResultModel<PetitionStatus>> CreateStatus(PetitionStatus petitionStatis, int userID)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<PetitionStatus>> UpdateStatus(PetitionStatus petitionStatis, int userID)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<Petition>> UpdateStatusPetititon(int idStatus, int PetitionID, int userID)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel<string>> validateStatus(int idStatus)
        {
            throw new NotImplementedException();
        }
    }
}