using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.RequestSystem.Entities.ModelsAdmin
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? AreaBoss1 { get; set; }
        public string? AreaBoss2 { get; set; }
    }
}
