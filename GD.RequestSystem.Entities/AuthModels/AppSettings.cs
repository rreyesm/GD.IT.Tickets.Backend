using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.RequestSystem.Entities.AuthModels
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string ConnectionString { get; set; }
    }
}
