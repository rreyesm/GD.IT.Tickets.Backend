using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.QualityAssurance.Entities.ModelsAdmin
{
    public class Profile
    {
        public Profile()
        {
            Permissions = new();
        }

        [Key]
        public int ProfileID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        [ForeignKey("ProjectID")]
        public int ProjectID { get; set; }
        public Project? Project { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}
