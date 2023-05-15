using System.Net;

namespace Shared.Wrapper
{
    public class Result : IResult
    {
        public List<string> Messages { get; set; } = new List<string>();
        public bool Succeeded { get; set; }
        public int StatusCode { get; set; }

        public static IResult Fail(int statusCode = (int)HttpStatusCode.OK)
        {
            return new Result { Succeeded = false, StatusCode = statusCode };
        }

        public static IResult Fail(string message, int statusCode = (int)HttpStatusCode.OK)
        {
            return new Result { Succeeded = false, Messages = new List<string> { message }, StatusCode = statusCode };
        }

        public static IResult Fail(List<string> messages, int statusCode = (int)HttpStatusCode.OK)
        {
            return new Result { Succeeded = false, Messages = messages, StatusCode = statusCode };
        }

        public static Task<IResult> FailAsync(int statusCode = (int)HttpStatusCode.OK)
        {
            return Task.FromResult(Fail(statusCode));
        }

        public static Task<IResult> FailAsync(string message, int statusCode = (int)HttpStatusCode.OK)
        {
            return Task.FromResult(Fail(message, statusCode));
        }

        public static Task<IResult> FailAsync(List<string> messages, int statusCode = (int)HttpStatusCode.OK)
        {
            return Task.FromResult(Fail(messages, statusCode));
        }

        public static IResult Success(int statusCode = (int)HttpStatusCode.OK)
        {
            return new Result { Succeeded = true, StatusCode = statusCode };
        }

        public static IResult Success(string message, int statusCode = (int)HttpStatusCode.OK)
        {
            return new Result { Succeeded = true, Messages = new List<string> { message }, StatusCode = statusCode };
        }

        public static Task<IResult> SuccessAsync(int statusCode = (int)HttpStatusCode.OK)
        {
            return Task.FromResult(Success(statusCode));
        }

        public static Task<IResult> SuccessAsync(string message, int statusCode = (int)HttpStatusCode.OK)
        {
            return Task.FromResult(Success(message, statusCode));
        }
    }

    public class Result<T> : Result, IResult<T>
    {
        public T Data { get; set; }

        public new static Result<T> Fail(int statusCode = (int)HttpStatusCode.OK)
        {
            return new Result<T> { Succeeded = false, StatusCode = statusCode };
        }

        public new static Result<T> Fail(string message, int statusCode = (int)HttpStatusCode.OK)
        {
            return new Result<T> { Succeeded = false, Messages = new List<string> { message }, StatusCode = statusCode };
        }

        public static Result<T> Fail(string message, T data, int statusCode = (int)HttpStatusCode.OK)
        {
            return new Result<T> { Succeeded = false, Data = data, Messages = new List<string> { message }, StatusCode = statusCode };
        }

        public new static Result<T> Fail(List<string> messages, int statusCode = (int)HttpStatusCode.OK)
        {
            return new Result<T> { Succeeded = false, Messages = messages, StatusCode = statusCode };
        }

        public static Result<T> Fail(List<string> messages, T data, int statusCode = (int)HttpStatusCode.OK)
        {
            return new Result<T> { Succeeded = false, Data = data, Messages = messages, StatusCode = statusCode };
        }

        public new static Task<Result<T>> FailAsync(int statusCode = (int)HttpStatusCode.OK)
        {
            return Task.FromResult(Fail(statusCode));
        }

        public new static Task<Result<T>> FailAsync(string message, int statusCode = (int)HttpStatusCode.OK)
        {
            return Task.FromResult(Fail(message, statusCode));
        }

        public static Task<Result<T>> FailAsync(string messages, T data, int statusCode = (int)HttpStatusCode.OK)
        {
            return Task.FromResult(Fail(messages, data, statusCode));
        }

        public new static Task<Result<T>> FailAsync(List<string> messages, int statusCode = (int)HttpStatusCode.OK)
        {
            return Task.FromResult(Fail(messages, statusCode));
        }

        public static Task<Result<T>> FailAsync(List<string> messages, T data, int statusCode = (int)HttpStatusCode.OK)
        {
            return Task.FromResult(Fail(messages, data, statusCode));
        }

        public new static Result<T> Success(int statusCode = (int)HttpStatusCode.OK)
        {
            return new Result<T> { Succeeded = true, StatusCode = statusCode };
        }

        public new static Result<T> Success(string message, int statusCode = (int)HttpStatusCode.OK)
        {
            return new Result<T> { Succeeded = true, Messages = new List<string> { message }, StatusCode = statusCode };
        }

        public static Result<T> Success(T data, int statusCode = (int)HttpStatusCode.OK)
        {
            return new Result<T> { Succeeded = true, Data = data, StatusCode = statusCode };
        }

        public static Result<T> Success(T data, string message, int statusCode = (int)HttpStatusCode.OK)
        {
            return new Result<T> { Succeeded = true, Data = data, Messages = new List<string> { message }, StatusCode = statusCode };
        }

        public static Result<T> Success(T data, List<string> messages, int statusCode = (int)HttpStatusCode.OK)
        {
            return new Result<T> { Succeeded = true, Data = data, Messages = messages, StatusCode = statusCode };
        }

        public new static Task<Result<T>> SuccessAsync(int statusCode = (int)HttpStatusCode.OK)
        {
            return Task.FromResult(Success(statusCode));
        }

        public new static Task<Result<T>> SuccessAsync(string message, int statusCode = (int)HttpStatusCode.OK)
        {
            return Task.FromResult(Success(message, statusCode));
        }

        public static Task<Result<T>> SuccessAsync(T data, int statusCode = (int)HttpStatusCode.OK)
        {
            return Task.FromResult(Success(data, statusCode));
        }

        public static Task<Result<T>> SuccessAsync(T data, List<string> messages, int statusCode = (int)HttpStatusCode.OK)
        {
            return Task.FromResult(Success(data, messages, statusCode));
        }

        public static Task<Result<T>> SuccessAsync(T data, string message, int statusCode = (int)HttpStatusCode.OK)
        {
            return Task.FromResult(Success(data, message, statusCode));
        }
    }
}