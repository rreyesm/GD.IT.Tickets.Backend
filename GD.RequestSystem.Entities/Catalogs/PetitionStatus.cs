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
    [Table("tblPetitionStatus")]
    public class PetitionStatus
    {
        [Key]
        public int PetitionStatusID { get; set; }
        public string? PetitionStatusName { get; set; }
        public string? PetitionStatusKey { get; set; }
        public int? CreaterID { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? LastUpdaterID { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? DeleterID { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool Status { get; set; }
    }
}
