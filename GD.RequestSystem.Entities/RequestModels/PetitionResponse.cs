using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.RequestSystem.Entities.RequestModels
{
    [NotMapped]
    public class PetitionResponse
    {
        public int PetitionID { get; set; }
        public string PetitionTitle { get; set; }
        public int userCreateID { get; set; }
        public string MessagePetition { get; set; }
        public DateTime DateTime { get; set; }
        public int? PetitionStatus { get; set; }
        public string? urlDocumentPetition { get; set; }
        public int? AreaID { get; set; }
        public int? userAsignedID { get; set; }
        public int? ResultPetitionID { get; set; }
        public int? ResponsePetitionId { get; set; }
        public string MessageResponce { get; set; }
        public int? userResponceID { get; set; }
        public DateTime? DateTimeResponse { get; set; }
        public string? urlDocumentResponse { get; set; }
    }
}
