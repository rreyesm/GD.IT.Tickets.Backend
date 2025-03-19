using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.QualityAssurance.Entities.ModelsAdmin
{
    public class Project : GeneralEntity 
    {
        public Project()
        {
            Profiles = new();
            Modules = new();
        }

        [Key]
        public int ProjectID { get; set; }
        public string? Name { get; set; }
        public List<Profile> Profiles { get; set; }
        public List<Module> Modules { get; set; }
    }
}
