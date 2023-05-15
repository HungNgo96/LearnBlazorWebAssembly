using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Model.Http
{
    public class BaseResponse
    {
        public int StatusCode { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public bool Succeeded { get; set; }
    }

    public class BaseResponse<T>: BaseResponse
    {
        public T Data { get; set; }
    }
}
