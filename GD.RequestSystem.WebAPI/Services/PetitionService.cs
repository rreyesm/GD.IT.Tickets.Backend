using System.Data;
using GD.QualityAssurance.Entities.AuthModels;
using GD.QualityAssurance.Entities.Shared;
using GD.RequestSystem.DAL.EF;
using GD.RequestSystem.Entities;
using GD.RequestSystem.Entities.RequestModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GD.RequestSystem.WebAPI.Services
{
    public interface IPetitionService
    {
        Task<ResultModel<Petition>> CreatePetition(Petition petition, int userID);
        Task<ResultModel<List<PetitionResponse>>> GetAllPetitions();
        Task<ResultModel<PetitionResponse>> GetPetitionbyID(int petitionID);
        Task<ResultModel<List<PetitionResponse>>> GetAllPetitionByStatus(int statiID);
        Task<ResultModel<List<PetitionResponse>>> GetAllPetitionByArea(int areaID);
        Task<ResultModel<List<PetitionResponse>>> GetAllPetitionByUserID(int userID);

    }
    public class PetitionService : IPetitionService
    {
        private readonly AppSettings _appSettings;
        private readonly RequestSystemDbContext _context;
        public PetitionService(RequestSystemDbContext context, IOptions<AppSettings> options)
        {
            _context = context;
            _appSettings = options.Value;
        }
        public async Task<ResultModel<Petition>> CreatePetition(Petition petition, int userID)
        {

            using var connection = _context.Database.GetDbConnection();
            var query = @"INSERT INTO tblPetition
                (PetitionTitle,userCreateID, message, DateTime, PetitionStatus,urlDocument, 
                AreaID, CreaterID,CreateDate,Status)
                VALUES (@PetitionTitle, @UserCreateID, @Message, @DateTime, @PetitionStatus, 
                @urlDocument,@AreaID, @CreaterID, @CreateDate, @Status)";

            using var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.Add(new SqlParameter("@PetitionTitle", petition.PetitionTitle));
            command.Parameters.Add(new SqlParameter("@UserCreateID", userID));
            command.Parameters.Add(new SqlParameter("@Message", petition.MessagePetition));
            command.Parameters.Add(new SqlParameter("@DateTime", petition.DateTime));
            command.Parameters.Add(new SqlParameter("@PetitionStatus", petition.PetitionStatus));
            command.Parameters.Add(new SqlParameter("@AreaID", petition.AreaID));
            command.Parameters.Add(new SqlParameter("@urlDocument", petition.urlDocumentPetition));
            command.Parameters.Add(new SqlParameter("@CreaterID", userID));
            command.Parameters.Add(new SqlParameter("@CreateDate", petition.CreateDate));
            command.Parameters.Add(new SqlParameter("@Status", petition.Status));

            await connection.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();
            if (rowsAffected > 0)
            {
                return new ResultModel<Petition>
                {
                    Response = true,
                    Data = petition,
                    Message = "La peticion fue enviada con exito"
                };
            }
            else
            {
                return new ResultModel<Petition>
                {
                    Response = false,
                    Data = null,
                    Message = "No se pudo enviar la petición"
                };
            }


        }
        public async Task<ResultModel<List<PetitionResponse>>> GetAllPetitions()
        {
            using var connection = _context.Database.GetDbConnection();
            var query = @"SELECT p.PetitionID,p.PetitionTitle,p.userCreateID,
            p.MessagePetition,p.DateTime,p.PetitionStatus,p.urlDocumentPetition,
            p.AreaID,p.userAsignedID,o.ResultPetitionID, o.PetitionId,o.MessageResponce,
            o.userResponceID,o.DateTimeResponse,o.urlDocumentResponse 
            FROM tblPetition p LEFT JOIN tblResultPetition o on p.PetitionID = o.PetitionId";

            using var command = connection.CreateCommand();
            command.CommandText = query;

            await connection.OpenAsync();
            var result = await command.ExecuteReaderAsync();

            var petitions = new List<PetitionResponse>();

            if (result.HasRows)
            {
                while (await result.ReadAsync())
                {
                    var petition = new PetitionResponse
                    {
                        // Campos de tblPetition
                        PetitionID = result.IsDBNull(result.GetOrdinal("PetitionID")) ? 0 : result.GetInt32(result.GetOrdinal("PetitionID")),
                        PetitionTitle = result.IsDBNull(result.GetOrdinal("PetitionTitle")) ? null : result.GetString(result.GetOrdinal("PetitionTitle")),
                        userCreateID = result.IsDBNull(result.GetOrdinal("userCreateID")) ? 0 : result.GetInt32(result.GetOrdinal("userCreateID")),
                        MessagePetition = result.IsDBNull(result.GetOrdinal("MessagePetition")) ? null : result.GetString(result.GetOrdinal("MessagePetition")),
                        DateTime = result.IsDBNull(result.GetOrdinal("DateTime")) ? DateTime.MinValue : result.GetDateTime(result.GetOrdinal("DateTime")),
                        PetitionStatus = result.IsDBNull(result.GetOrdinal("PetitionStatus")) ? 0 : result.GetInt32(result.GetOrdinal("PetitionStatus")),
                        urlDocumentPetition = result.IsDBNull(result.GetOrdinal("urlDocumentPetition")) ? null : result.GetString(result.GetOrdinal("urlDocumentPetition")),
                        AreaID = result.IsDBNull(result.GetOrdinal("AreaID")) ? 0 : result.GetInt32(result.GetOrdinal("AreaID")),
                        userAsignedID = result.IsDBNull(result.GetOrdinal("userAsignedID")) ? 0 : result.GetInt32(result.GetOrdinal("userAsignedID")),

                        // Campos de tblResultPetition
                        ResultPetitionID = result.IsDBNull(result.GetOrdinal("ResultPetitionID")) ? 0 : result.GetInt32(result.GetOrdinal("ResultPetitionID")),
                        ResponsePetitionId = result.IsDBNull(result.GetOrdinal("PetitionId")) ? 0 : result.GetInt32(result.GetOrdinal("PetitionId")),
                        MessageResponce = result.IsDBNull(result.GetOrdinal("MessageResponce")) ? null : result.GetString(result.GetOrdinal("MessageResponce")),
                        userResponceID = result.IsDBNull(result.GetOrdinal("userResponceID")) ? 0 : result.GetInt32(result.GetOrdinal("userResponceID")),
                        DateTimeResponse = result.IsDBNull(result.GetOrdinal("DateTimeResponse")) ? (DateTime?)null : result.GetDateTime(result.GetOrdinal("DateTimeResponse")),
                        urlDocumentResponse = result.IsDBNull(result.GetOrdinal("UrlDocumentResponse")) ? null : result.GetString(result.GetOrdinal("UrlDocumentResponse"))
                    };

                    petitions.Add(petition);
                }

                await result.CloseAsync();
                await connection.CloseAsync();

                return new ResultModel<List<PetitionResponse>>
                {
                    Response = true,
                    Data = petitions,
                    Message = "Peticiones encontradas"
                };
            }
            else
            {
                await result.CloseAsync();
                await connection.CloseAsync();

                return new ResultModel<List<PetitionResponse>>
                {
                    Response = false,
                    Data = null,
                    Message = "No hay peticiones"
                };
            }
        }

        public async Task<ResultModel<PetitionResponse>> GetPetitionbyID(int petitionID)
        {
            using var connection = _context.Database.GetDbConnection();
            var query = @"SELECT p.PetitionID,p.PetitionTitle,p.userCreateID,
                        p.MessagePetition,p.DateTime,p.PetitionStatus,p.urlDocumentPetition,
                        p.AreaID,p.userAsignedID,o.ResultPetitionID, o.PetitionId,
                        o.MessageResponce,o.userResponceID,o.DateTimeResponse,o.urlDocumentResponse
			            FROM tblPetition p
			            LEFT JOIN tblResultPetition o ON p.PetitionID = o.PetitionId
			            WHERE p.PetitionID = @petitionID";
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.Add(new SqlParameter("@PetitionID", petitionID));

            await connection.OpenAsync();
            var result = await command.ExecuteReaderAsync();

            var petition = new PetitionResponse();
            if (result.HasRows)
            {
                while (await result.ReadAsync())
                {
                    petition = new PetitionResponse
                    {
                        // Campos de tblPetition
                        PetitionID = result.IsDBNull(result.GetOrdinal("PetitionID")) ? 0 : result.GetInt32(result.GetOrdinal("PetitionID")),
                        PetitionTitle = result.IsDBNull(result.GetOrdinal("PetitionTitle")) ? null : result.GetString(result.GetOrdinal("PetitionTitle")),
                        userCreateID = result.IsDBNull(result.GetOrdinal("userCreateID")) ? 0 : result.GetInt32(result.GetOrdinal("userCreateID")),
                        MessagePetition = result.IsDBNull(result.GetOrdinal("MessagePetition")) ? null : result.GetString(result.GetOrdinal("MessagePetition")),
                        DateTime = result.IsDBNull(result.GetOrdinal("DateTime")) ? DateTime.MinValue : result.GetDateTime(result.GetOrdinal("DateTime")),
                        PetitionStatus = result.IsDBNull(result.GetOrdinal("PetitionStatus")) ? 0 : result.GetInt32(result.GetOrdinal("PetitionStatus")),
                        urlDocumentPetition = result.IsDBNull(result.GetOrdinal("urlDocumentPetition")) ? null : result.GetString(result.GetOrdinal("urlDocumentPetition")),
                        AreaID = result.IsDBNull(result.GetOrdinal("AreaID")) ? 0 : result.GetInt32(result.GetOrdinal("AreaID")),
                        userAsignedID = result.IsDBNull(result.GetOrdinal("userAsignedID")) ? 0 : result.GetInt32(result.GetOrdinal("userAsignedID")),

                        // Campos de tblResultPetition
                        ResultPetitionID = result.IsDBNull(result.GetOrdinal("ResultPetitionID")) ? 0 : result.GetInt32(result.GetOrdinal("ResultPetitionID")),
                        ResponsePetitionId = result.IsDBNull(result.GetOrdinal("PetitionId")) ? 0 : result.GetInt32(result.GetOrdinal("PetitionId")),
                        MessageResponce = result.IsDBNull(result.GetOrdinal("MessageResponce")) ? null : result.GetString(result.GetOrdinal("MessageResponce")),
                        userResponceID = result.IsDBNull(result.GetOrdinal("userResponceID")) ? 0 : result.GetInt32(result.GetOrdinal("userResponceID")),
                        DateTimeResponse = result.IsDBNull(result.GetOrdinal("DateTimeResponse")) ? (DateTime?)null : result.GetDateTime(result.GetOrdinal("DateTimeResponse")),
                        urlDocumentResponse = result.IsDBNull(result.GetOrdinal("UrlDocumentResponse")) ? null : result.GetString(result.GetOrdinal("UrlDocumentResponse"))
                    };

                }

                await result.CloseAsync();
                await connection.CloseAsync();

                return new ResultModel<PetitionResponse>
                {
                    Response = true,
                    Data = petition,
                    Message = "Peticiones encontradas"
                };
            }
            else
            {
                await result.CloseAsync();
                await connection.CloseAsync();

                return new ResultModel<PetitionResponse>
                {
                    Response = false,
                    Data = null,
                    Message = "No hay peticiones"
                };
            }
        }

        public async Task<ResultModel<List<PetitionResponse>>> GetAllPetitionByStatus(int statusID)
        {
            using var connection = _context.Database.GetDbConnection();
            var query = @"SELECT  p.PetitionID,p.PetitionTitle,p.userCreateID,p.MessagePetition,
                        p.DateTime,p.PetitionStatus,p.urlDocumentPetition,p.AreaID,p.userAsignedID,
                        o.ResultPetitionID, o.PetitionId,o.MessageResponce,o.userResponceID,o.DateTimeResponse,
                        o.urlDocumentResponse
			            FROM tblPetition p
			            LEFT JOIN tblResultPetition o ON p.PetitionID = o.PetitionID
			            WHERE p.PetitionStatus = @PetitionStatus";

            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.Add(new SqlParameter("@PetitionStatus", statusID));

            await connection.OpenAsync();
            var result = await command.ExecuteReaderAsync();

            var petitions = new List<PetitionResponse>();

            if (result.HasRows)
            {
                while (await result.ReadAsync())
                {
                    var petition = new PetitionResponse
                    {
                        // Campos de tblPetition
                        PetitionID = result.IsDBNull(result.GetOrdinal("PetitionID")) ? 0 : result.GetInt32(result.GetOrdinal("PetitionID")),
                        PetitionTitle = result.IsDBNull(result.GetOrdinal("PetitionTitle")) ? null : result.GetString(result.GetOrdinal("PetitionTitle")),
                        userCreateID = result.IsDBNull(result.GetOrdinal("userCreateID")) ? 0 : result.GetInt32(result.GetOrdinal("userCreateID")),
                        MessagePetition = result.IsDBNull(result.GetOrdinal("MessagePetition")) ? null : result.GetString(result.GetOrdinal("MessagePetition")),
                        DateTime = result.IsDBNull(result.GetOrdinal("DateTime")) ? DateTime.MinValue : result.GetDateTime(result.GetOrdinal("DateTime")),
                        PetitionStatus = result.IsDBNull(result.GetOrdinal("PetitionStatus")) ? 0 : result.GetInt32(result.GetOrdinal("PetitionStatus")),
                        urlDocumentPetition = result.IsDBNull(result.GetOrdinal("urlDocumentPetition")) ? null : result.GetString(result.GetOrdinal("urlDocumentPetition")),
                        AreaID = result.IsDBNull(result.GetOrdinal("AreaID")) ? 0 : result.GetInt32(result.GetOrdinal("AreaID")),
                        userAsignedID = result.IsDBNull(result.GetOrdinal("userAsignedID")) ? 0 : result.GetInt32(result.GetOrdinal("userAsignedID")),

                        // Campos de tblResultPetition
                        ResultPetitionID = result.IsDBNull(result.GetOrdinal("ResultPetitionID")) ? 0 : result.GetInt32(result.GetOrdinal("ResultPetitionID")),
                        ResponsePetitionId = result.IsDBNull(result.GetOrdinal("PetitionId")) ? 0 : result.GetInt32(result.GetOrdinal("PetitionId")),
                        MessageResponce = result.IsDBNull(result.GetOrdinal("MessageResponce")) ? null : result.GetString(result.GetOrdinal("MessageResponce")),
                        userResponceID = result.IsDBNull(result.GetOrdinal("userResponceID")) ? 0 : result.GetInt32(result.GetOrdinal("userResponceID")),
                        DateTimeResponse = result.IsDBNull(result.GetOrdinal("DateTimeResponse")) ? (DateTime?)null : result.GetDateTime(result.GetOrdinal("DateTimeResponse")),
                        urlDocumentResponse = result.IsDBNull(result.GetOrdinal("UrlDocumentResponse")) ? null : result.GetString(result.GetOrdinal("UrlDocumentResponse"))
                    };

                    petitions.Add(petition);
                }

                await result.CloseAsync();
                await connection.CloseAsync();

                return new ResultModel<List<PetitionResponse>>
                {
                    Response = true,
                    Data = petitions,
                    Message = "Peticiones encontradas"
                };
            }
            else
            {
                await result.CloseAsync();
                await connection.CloseAsync();

                return new ResultModel<List<PetitionResponse>>
                {
                    Response = false,
                    Data = null,
                    Message = "No hay peticiones"
                };
            }
        }

        public async Task<ResultModel<List<PetitionResponse>>> GetAllPetitionByArea(int areaID)
        {
            using var connection = _context.Database.GetDbConnection();
            var query = @"SELECT p.PetitionID,p.PetitionTitle,p.userCreateID,p.MessagePetition,
                        p.DateTime,p.PetitionStatus,p.urlDocumentPetition,p.AreaID,
                        p.userAsignedID,o.ResultPetitionID, o.PetitionId,o.MessageResponce,
                        o.userResponceID,o.DateTimeResponse,o.urlDocumentResponse
			            FROM tblPetition p
                        LEFT JOIN tblResultPetition o ON p.PetitionID = o.PetitionID
                        WHERE p.AreaID = @AreaID";
            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.Add(new SqlParameter("@AreaID", areaID));

            await connection.OpenAsync();
            var result = await command.ExecuteReaderAsync();

            var petitions = new List<PetitionResponse>();

            if (result.HasRows)
            {
                while (await result.ReadAsync())
                {
                    var petition = new PetitionResponse
                    {
                        // Campos de tblPetition
                        PetitionID = result.IsDBNull(result.GetOrdinal("PetitionID")) ? 0 : result.GetInt32(result.GetOrdinal("PetitionID")),
                        PetitionTitle = result.IsDBNull(result.GetOrdinal("PetitionTitle")) ? null : result.GetString(result.GetOrdinal("PetitionTitle")),
                        userCreateID = result.IsDBNull(result.GetOrdinal("userCreateID")) ? 0 : result.GetInt32(result.GetOrdinal("userCreateID")),
                        MessagePetition = result.IsDBNull(result.GetOrdinal("MessagePetition")) ? null : result.GetString(result.GetOrdinal("MessagePetition")),
                        DateTime = result.IsDBNull(result.GetOrdinal("DateTime")) ? DateTime.MinValue : result.GetDateTime(result.GetOrdinal("DateTime")),
                        PetitionStatus = result.IsDBNull(result.GetOrdinal("PetitionStatus")) ? 0 : result.GetInt32(result.GetOrdinal("PetitionStatus")),
                        urlDocumentPetition = result.IsDBNull(result.GetOrdinal("urlDocumentPetition")) ? null : result.GetString(result.GetOrdinal("urlDocumentPetition")),
                        AreaID = result.IsDBNull(result.GetOrdinal("AreaID")) ? 0 : result.GetInt32(result.GetOrdinal("AreaID")),
                        userAsignedID = result.IsDBNull(result.GetOrdinal("userAsignedID")) ? 0 : result.GetInt32(result.GetOrdinal("userAsignedID")),

                        // Campos de tblResultPetition
                        ResultPetitionID = result.IsDBNull(result.GetOrdinal("ResultPetitionID")) ? 0 : result.GetInt32(result.GetOrdinal("ResultPetitionID")),
                        ResponsePetitionId = result.IsDBNull(result.GetOrdinal("PetitionId")) ? 0 : result.GetInt32(result.GetOrdinal("PetitionId")),
                        MessageResponce = result.IsDBNull(result.GetOrdinal("MessageResponce")) ? null : result.GetString(result.GetOrdinal("MessageResponce")),
                        userResponceID = result.IsDBNull(result.GetOrdinal("userResponceID")) ? 0 : result.GetInt32(result.GetOrdinal("userResponceID")),
                        DateTimeResponse = result.IsDBNull(result.GetOrdinal("DateTimeResponse")) ? (DateTime?)null : result.GetDateTime(result.GetOrdinal("DateTimeResponse")),
                        urlDocumentResponse = result.IsDBNull(result.GetOrdinal("UrlDocumentResponse")) ? null : result.GetString(result.GetOrdinal("UrlDocumentResponse"))
                    };

                    petitions.Add(petition);
                }

                await result.CloseAsync();
                await connection.CloseAsync();

                return new ResultModel<List<PetitionResponse>>
                {
                    Response = true,
                    Data = petitions,
                    Message = "Peticiones encontradas"
                };
            }
            else
            {
                await result.CloseAsync();
                await connection.CloseAsync();

                return new ResultModel<List<PetitionResponse>>
                {
                    Response = false,
                    Data = null,
                    Message = "No hay peticiones"
                };
            }
        }
        public async Task<ResultModel<List<PetitionResponse>>> GetAllPetitionByUserID(int userID)
        {
            using var connection = _context.Database.GetDbConnection();
            var query = @"SELECT  p.PetitionID,p.PetitionTitle,p.userCreateID,p.MessagePetition,
                        p.DateTime,p.PetitionStatus,p.urlDocumentPetition,p.AreaID,p.userAsignedID,
                        o.ResultPetitionID, o.PetitionId,o.MessageResponce,o.userResponceID,o.DateTimeResponse,
                        o.urlDocumentResponse
			            FROM tblPetition p
			            LEFT JOIN tblResultPetition o ON p.PetitionID = o.PetitionID
			            WHERE p.userCreateID = @userCreateID";

            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.Add(new SqlParameter("@userCreateID", userID));

            await connection.OpenAsync();
            var result = await command.ExecuteReaderAsync();

            var petitions = new List<PetitionResponse>();

            if (result.HasRows)
            {
                while (await result.ReadAsync())
                {
                    var petition = new PetitionResponse
                    {
                        // Campos de tblPetition
                        PetitionID = result.IsDBNull(result.GetOrdinal("PetitionID")) ? 0 : result.GetInt32(result.GetOrdinal("PetitionID")),
                        PetitionTitle = result.IsDBNull(result.GetOrdinal("PetitionTitle")) ? null : result.GetString(result.GetOrdinal("PetitionTitle")),
                        userCreateID = result.IsDBNull(result.GetOrdinal("userCreateID")) ? 0 : result.GetInt32(result.GetOrdinal("userCreateID")),
                        MessagePetition = result.IsDBNull(result.GetOrdinal("MessagePetition")) ? null : result.GetString(result.GetOrdinal("MessagePetition")),
                        DateTime = result.IsDBNull(result.GetOrdinal("DateTime")) ? DateTime.MinValue : result.GetDateTime(result.GetOrdinal("DateTime")),
                        PetitionStatus = result.IsDBNull(result.GetOrdinal("PetitionStatus")) ? 0 : result.GetInt32(result.GetOrdinal("PetitionStatus")),
                        urlDocumentPetition = result.IsDBNull(result.GetOrdinal("urlDocumentPetition")) ? null : result.GetString(result.GetOrdinal("urlDocumentPetition")),
                        AreaID = result.IsDBNull(result.GetOrdinal("AreaID")) ? 0 : result.GetInt32(result.GetOrdinal("AreaID")),
                        userAsignedID = result.IsDBNull(result.GetOrdinal("userAsignedID")) ? 0 : result.GetInt32(result.GetOrdinal("userAsignedID")),

                        // Campos de tblResultPetition
                        ResultPetitionID = result.IsDBNull(result.GetOrdinal("ResultPetitionID")) ? 0 : result.GetInt32(result.GetOrdinal("ResultPetitionID")),
                        ResponsePetitionId = result.IsDBNull(result.GetOrdinal("PetitionId")) ? 0 : result.GetInt32(result.GetOrdinal("PetitionId")),
                        MessageResponce = result.IsDBNull(result.GetOrdinal("MessageResponce")) ? null : result.GetString(result.GetOrdinal("MessageResponce")),
                        userResponceID = result.IsDBNull(result.GetOrdinal("userResponceID")) ? 0 : result.GetInt32(result.GetOrdinal("userResponceID")),
                        DateTimeResponse = result.IsDBNull(result.GetOrdinal("DateTimeResponse")) ? (DateTime?)null : result.GetDateTime(result.GetOrdinal("DateTimeResponse")),
                        urlDocumentResponse = result.IsDBNull(result.GetOrdinal("UrlDocumentResponse")) ? null : result.GetString(result.GetOrdinal("UrlDocumentResponse"))
                    };

                    petitions.Add(petition);
                }

                await result.CloseAsync();
                await connection.CloseAsync();

                return new ResultModel<List<PetitionResponse>>
                {
                    Response = true,
                    Data = petitions,
                    Message = "Peticiones encontradas"
                };
            }
            else
            {
                await result.CloseAsync();
                await connection.CloseAsync();

                return new ResultModel<List<PetitionResponse>>
                {
                    Response = false,
                    Data = null,
                    Message = "No hay peticiones"
                };
            }
        }
    }
}
