using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.QualityAssurance.Entities.ModelsAdmin
{
    public class GeneralEntity
    {
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeletedDate { get; set; }

        public GeneralEntity()
        {
            CreatedDate = DateTime.Now;
            IsActive = true;
        }
    }
}
