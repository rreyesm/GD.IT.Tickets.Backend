using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GD.RequestSystem.Entities
{
    [Table("tblPetition")]
    public class Petition
    {
        [Key]
        public int PetitionID { get; set; }
        public string PetitionTitle { get; set; }
        public int userCreateID { get; set; }
        public string MessagePetition { get; set; }
        public DateTime DateTime { get; set; }
        public int? PetitionStatus { get; set; }
        public string? urlDocumentPetition { get; set; }
        public int? AreaID { get; set; }
        public int? userAsignedID { get; set; }
        public int? CreaterID { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? LastUpdaterID { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? DeleterID { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool Status { get; set; }
    }
}
