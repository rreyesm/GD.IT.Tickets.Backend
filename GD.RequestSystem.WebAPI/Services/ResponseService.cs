using GD.RequestSystem.Entities.AuthModels;
using GD.RequestSystem.Entities.Shared;
using GD.RequestSystem.DAL.EF;
using GD.RequestSystem.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GD.RequestSystem.WebAPI.Services
{
    public interface IResponseService
    {
        Task<ResultModel<ResultPetition>> CreateResponsePetition(ResultPetition resultPetition, int userID);
    }
    public class ResponseService : IResponseService
    {
        private readonly AppSettings _appSettings;
        private readonly RequestSystemDbContext _context;
        public ResponseService(RequestSystemDbContext context, IOptions<AppSettings> options)
        {
            _context = context;
            _appSettings = options.Value;
        }

        public async Task<ResultModel<ResultPetition>> CreateResponsePetition(ResultPetition resultPetition, int userID)
        {
            using var connection = _context.Database.GetDbConnection();
            var query = "@INSERT INTO tblResultPetition (PetitionID,ResponseMessage,CreaterPetitionID," +
                "userResponceID,DateTime,urlDocument,CreaterID,CreateDate,Status)" +
                "VALUES (@PetitionID,@ResponseMessage,@CreaterPetitionID,@userResponceID,@DateTime," +
                "@urlDocument,@CreaterID,@Status)";
            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.Add(new SqlParameter("@PetitionID", resultPetition.ResultPetitionID));
            command.Parameters.Add(new SqlParameter("@PetitionID", resultPetition.PetitionId));
            command.Parameters.Add(new SqlParameter("@ResponseMessage", resultPetition.MessageResponce));
            command.Parameters.Add(new SqlParameter("@userResponceID", resultPetition.userResponceID));
            command.Parameters.Add(new SqlParameter("@DateTime", resultPetition.DateTimeResponse));
            command.Parameters.Add(new SqlParameter("@urlDocument", resultPetition.UrlDocumentResponse));
            command.Parameters.Add(new SqlParameter("@CreaterID", resultPetition.CreaterID));
            command.Parameters.Add(new SqlParameter("@CreateDate", resultPetition.CreateDate));
            command.Parameters.Add(new SqlParameter("@Status", resultPetition.Status));

            await connection.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();
            if (rowsAffected > 0)
            {
                return new ResultModel<ResultPetition>
                {
                    Response = true,
                    Data = resultPetition,
                    Message = "La Respuesta fue enviada con exito"
                };
            }
            else
            {
                return new ResultModel<ResultPetition>
                {
                    Response = false,
                    Data = null,
                    Message = "No se pudo enviar la respuesta"
                };
            }
        }
    }
}
