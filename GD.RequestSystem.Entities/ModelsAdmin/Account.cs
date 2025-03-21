using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.RequestSystem.Entities.ModelsAdmin
{
    public class Account : GeneralEntity
    {
        [Key]
        public int AccountID { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public User? User { get; set; }
        public int DepartmentID { get; set; }
        public Department? Departament { get; set; }

        [ForeignKey("ProjectID")]
        public int ProjectID { get; set; }
        public Project? Project { get; set; }

        [ForeignKey("ProfileID")]
        public int ProfileID { get; set; }
        public Profile? Profile { get; set; }

    }
}
