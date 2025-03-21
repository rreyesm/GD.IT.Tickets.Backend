using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.RequestSystem.Entities.ModelsAdmin
{
    public class Permission
    {
        public Permission()
        {
        }

        [Key]
        public int PermissionID { get; set; }

        public int LineNum { get; set; }

        [ForeignKey("ProfileID")]
        public int ProfileID { get; set; }
        public Profile? Profile { get; set; }
        public string? ProfileName { get; set; }


        [ForeignKey("ModuleID")]
        public int ModuleID { get; set; }
        public Module? Module { get; set; }
        public string? ModuleName { get; set; }

        public bool Show { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool All { get; set; }

        public static implicit operator List<object>(Permission v)
        {
            throw new NotImplementedException();
        }
    }
}
