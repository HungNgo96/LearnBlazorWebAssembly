using System.Collections.Generic;

namespace Shared.Wrapper
{
    public interface IResult<out T> : IResult
    {
        T Data { get; }
    }

    public interface IResult
    {
        List<string> Messages { get; set; }
        bool Succeeded { get; set; }
        int StatusCode { get; set; }
    }
}