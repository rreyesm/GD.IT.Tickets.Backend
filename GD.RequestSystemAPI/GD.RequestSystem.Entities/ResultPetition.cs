using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.RequestSystem.Entities
{
    [Table("tblResultPetition")]
    public class ResultPetition
    {
        [Key]
        public int ResultPetitionID { get; set; }
        public int PetitionId { get; set; }
        public string MessageResponce { get; set; }
        public int userResponceID { get; set; }
        public DateTime? DateTimeResponse { get; set; }
        public string? UrlDocumentResponse { get; set; }
        public int? CreaterID { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? LastUpdaterID { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? DeleterID { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool Status { get; set; }
    }
}
