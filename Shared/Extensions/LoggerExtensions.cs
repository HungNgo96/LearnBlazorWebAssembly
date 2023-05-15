using Microsoft.Extensions.Logging;

namespace Shared.Extensions
{
    internal static class EventIds
    {
        public const int Request = 11;
        public const int ErrorResult = 12;
        public const int HttpErrorResult = 13;
        public const int Response = 14;
        public const int Error = 15;
        public const int Info = 16;
        public const int Warning = 17;
        public const int Debug = 18;
        public const int Add = 101;
        public const int Added = 102;
        public const int Update = 201;
        public const int Updated = 202;
        public const int Delete = 301;
        public const int Deleted = 302;
        public const int Job = 401;
    }

    public static class LoggerExtensions
    {
        private static readonly Action<ILogger, string, string, string, string, Exception> _request;
        private static readonly Action<ILogger, string, string, Exception> _requestWithoutParams;
        private static readonly Action<ILogger, string, string, string, Exception> _response;
        private static readonly Action<ILogger, string, string, Exception> _responseWithoutParams;
        private static readonly Action<ILogger, string, string, string, Exception> _errorResult;
        private static readonly Action<ILogger, string, string, string, Exception> _httpErrorResult;
        private static readonly Action<ILogger, string, string, string, Exception> _error;
        private static readonly Action<ILogger, string, string, string, Exception> _errorException;
        private static readonly Action<ILogger, string, string, string, Exception> _info;
        private static readonly Action<ILogger, string, string, string, Exception> _warning;
        private static readonly Action<ILogger, string, string, string, Exception> _debug;
        private static readonly Action<ILogger, string, string, string, Exception> _job;
        private static readonly Action<ILogger, string, string, string, Exception> _jobError;

        static LoggerExtensions()
        {
            _request = LoggerMessage.Define<string, string, string, string>(
                LogLevel.Information,
                new EventId(EventIds.Request, nameof(Request)),
                "{ClassName} -- {MethodName} -- request {RequestName}: {Parameters}.");

            _requestWithoutParams = LoggerMessage.Define<string, string>(
                LogLevel.Information,
                new EventId(EventIds.Request, nameof(Request)),
                "{ClassName} -- {MethodName} -- request.");

            _response = LoggerMessage.Define<string, string, string>(
                LogLevel.Information,
                new EventId(EventIds.Response, nameof(Response)),
                "{ClassName} -- {MethodName} -- response {Message}.");

            _responseWithoutParams = LoggerMessage.Define<string, string>(
                LogLevel.Information,
                new EventId(EventIds.Response, nameof(Response)),
                "{ClassName} -- {MethodName} -- response.");

            _errorResult = LoggerMessage.Define<string, string, string>(
                LogLevel.Error,
                new EventId(EventIds.ErrorResult, nameof(ErrorResult)),
                "{ClassName} -- {MethodName} -- response error result: {Message}.");

            _httpErrorResult = LoggerMessage.Define<string, string, string>(
                LogLevel.Error,
                new EventId(EventIds.HttpErrorResult, nameof(HttpErrorResult)),
                "{ClassName} -- {MethodName} -- response http error result: {Message}.");

            _error = LoggerMessage.Define<string, string, string>(
                LogLevel.Error,
                new EventId(EventIds.Error, nameof(Error)),
                "{ClassName} -- {MethodName} -- error: {Message}.");

            _errorException = LoggerMessage.Define<string, string, string>(
                LogLevel.Error,
                new EventId(EventIds.Error, nameof(Error)),
                "{ClassName} -- {MethodName} -- error: {Message}.");

            _info = LoggerMessage.Define<string, string, string>(
                LogLevel.Information,
                new EventId(EventIds.Info, nameof(Info)),
                "{ClassName} -- {MethodName} -- info: {Message}.");

            _warning = LoggerMessage.Define<string, string, string>(
                LogLevel.Warning,
                new EventId(EventIds.Warning, nameof(Warning)),
                "{ClassName} -- {MethodName} -- warning: {Message}.");

            _debug = LoggerMessage.Define<string, string, string>(
                LogLevel.Debug,
                new EventId(EventIds.Debug, nameof(Debug)),
                "{ClassName} -- {MethodName} -- debug: {Message}.");
            _job = LoggerMessage.Define<string, string, string>(
                LogLevel.Warning,
                new EventId(EventIds.Job, nameof(Job)),
                "------------QRTZ------------ {ClassName} -- {MethodName} -- Jobs: {Message}.");
            _jobError = LoggerMessage.Define<string, string, string>(
                LogLevel.Error,
                new EventId(EventIds.Job, nameof(Job)),
                "------------QRTZ------------ {ClassName} -- {MethodName} -- Jobs: {Message}.");
        }

        public static void Request(this ILogger logger, string className, string methodName, Exception e = default)
        {
            _requestWithoutParams(logger, className, methodName, e);
        }

        public static void Request(this ILogger logger, string className, string methodName, string requestName, string parameters, Exception e = default)
        {
            _request(logger, className, methodName, requestName, parameters, e);
        }

        public static void Response(this ILogger logger, string className, string methodName, Exception e = default)
        {
            _responseWithoutParams(logger, className, methodName, e);
        }

        public static void Response(this ILogger logger, string className, string methodName, string message, Exception e = default)
        {
            _response(logger, className, methodName, message, e);
        }

        public static void ErrorResult(this ILogger logger, string className, string methodName, string message, Exception e = default)
        {
            _errorResult(logger, className, methodName, message, e);
        }

        public static void HttpErrorResult(this ILogger logger, string className, string methodName, string message, Exception e = default)
        {
            _httpErrorResult(logger, className, methodName, message, e);
        }

        public static void Error(this ILogger logger, string className, string methodName, string message, Exception e = default)
        {
            _error(logger, className, methodName, message, e);
        }

        public static void ErrorException(this ILogger logger, string className, string methodName, Exception e = default)
        {
            _errorException(logger, className, methodName, string.Format("Message: {0} -- StackTrace: {1}", e.Message, e.StackTrace), e);
        }

        public static void Info(this ILogger logger, string className, string methodName, string message, Exception e = default)
        {
            _info(logger, className, methodName, message, e);
        }

        public static void Warning(this ILogger logger, string className, string methodName, string message, Exception e = default)
        {
            _warning(logger, className, methodName, message, e);
        }

        public static void Debug(this ILogger logger, string className, string methodName, string message, Exception e = default)
        {
            _debug(logger, className, methodName, message, e);
        }

        public static void Job(this ILogger logger, string className, string methodName, string message, Exception e = default)
        {
            _job(logger, className, methodName, message, e);
        }

        public static void JobError(this ILogger logger, string className, string methodName, string message, Exception e = default)
        {
            _jobError(logger, className, methodName, message, e);
        }
    }
}