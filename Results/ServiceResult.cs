using System.Net;
using System.Text.Json.Serialization;

namespace VolunteerHub.Results
{
    public interface IServiceResult
    {
        bool IsSuccess { get; set; }
        HttpStatusCode HttpStatusCode { get; set; }
        string? ExceptionMessage { get; set; }
    }

    public class ServiceResult<T> : IServiceResult where T : class
    {
        public T Data { get; set; }
        public string? ExceptionDetails { get; set; } 
        public string? ExceptionMessage { get; set; }
        public bool IsSuccess { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }

        public ServiceResult(T data, bool isSuccess, HttpStatusCode statusCode, string? exceptionMessage = null, string? exception = null)
        {
            Data = data;
            ExceptionDetails = exception;
            ExceptionMessage = exceptionMessage;
            IsSuccess = isSuccess;
            HttpStatusCode = statusCode;
        }

        [JsonConstructor]
        public ServiceResult() { }

        public static ServiceResult<T> CreateSuccess(T data)
        {
            return new ServiceResult<T>(data, true, HttpStatusCode.OK);
        }

        public static ServiceResult<T> CreateError(string? exceptionMessage = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest, string? exception = null)
        {
            return new ServiceResult<T>(default, false, statusCode, exceptionMessage, exception);
        }
    }

    public class ServiceResult : IServiceResult
    {
        public string? ExceptionDetails { get; set; }
        public string? ExceptionMessage { get; set; }
        public bool IsSuccess { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }

        public ServiceResult(bool isSuccess, HttpStatusCode statusCode, string? exceptionMessage = null, string? exception = null)
        {
            ExceptionDetails = exception;
            ExceptionMessage = exceptionMessage;
            IsSuccess = isSuccess;
            HttpStatusCode = statusCode;
        }

        [JsonConstructor]
        public ServiceResult() { }

        public static ServiceResult CreateSuccess()
        {
            return new ServiceResult(true, HttpStatusCode.OK);
        }

        public static ServiceResult CreateError(string? exceptionMessage = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest, string? exception = null)
        {
            return new ServiceResult(false, statusCode, exceptionMessage, exception);
        }
    }
}
