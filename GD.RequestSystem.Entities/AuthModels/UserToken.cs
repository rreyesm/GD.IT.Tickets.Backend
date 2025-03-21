using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.RequestSystem.Entities.AuthModels
{
  public class UserToken
  {
    public override string ToString()
    {
      return $"{Name}";
    }

    public int UserID { get; set; }
    public string Name { get; set; }
    public int DepartmentID{ get; set; }
    public string Department { get; set; }  
    public string roll { get;set; }
    public IList<string> Roles { get; set; } = new List<string>();
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
  }
}
