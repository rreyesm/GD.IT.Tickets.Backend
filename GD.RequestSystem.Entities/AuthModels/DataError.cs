using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GD.RequestSystem.Entities.AuthModels
{
  public class DataError
  {
    public override string ToString()
    {
      return $"{Message}";
    }
    public int MessageId { get; set; }
    public string? Message { get; set; }

  }
}
