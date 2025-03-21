using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.RequestSystem.Entities.ModelsAdmin
{
    public class Module : GeneralEntity
    {
        [Key]
        public int ModuleID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        [ForeignKey("ProjectID")]
        public int ProjectID { get; set; }
        public Project? Project { get; set; }
    }
}
