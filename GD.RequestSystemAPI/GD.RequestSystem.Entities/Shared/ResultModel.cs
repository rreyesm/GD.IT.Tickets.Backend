//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;

//namespace GD.QualityAssurance.Entities.Shared
//{
//    public class ResultModel<T>
//    {
//        public ResultModel()
//        {
//            Message = string.Empty;
//        }
//        public ResultModel(T data)
//        {
//            Data = data;
//        }

//        public bool Response { get; set; }
//        public string Message { get; set; }
//        public T Data { get; set; }
//        public int ResultType { get; set; }
//    }
//}

using System.Text.Json.Serialization;

namespace GD.QualityAssurance.Entities.Shared
{
  public class ResultModel<T>
  {
    public ResultModel()
    {
      Response = true;
      Data = default;
      Message = string.Empty;
    }

    public ResultModel(T data)
    {
      Response = true;
      Data = data;
      Message = string.Empty;
    }

    [JsonPropertyName("response")]
    public bool Response { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("Data")]
    public T? Data { get; set; }
  }
}
