using System.Net;
using System.Text.Json.Serialization;

namespace VolunteerHub.Results
{
    public class ServiceResult<T> where T : class
    {
        public T Data { get; set; }
        public Exception? Exception { get; set; }
        public string? ExceptionMessage { get; set; }
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ServiceResult(T data, bool isSuccess, HttpStatusCode statusCode, Exception? exception = null, string? exceptionMessage = null)
        {
            Data = data;
            Exception = exception;
            ExceptionMessage = exceptionMessage;
            IsSuccess = isSuccess;
            StatusCode = statusCode;
        }

        [JsonConstructor]
        public ServiceResult() { }

        public static ServiceResult<T> CreateSuccess(T data)
        {
            return new ServiceResult<T>(data, true, HttpStatusCode.OK);
        }

        public static ServiceResult<T> CreateError(bool isSuccess, Exception ex, string? exceptionMessage = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>(default, false, statusCode, ex, exceptionMessage);
        }
    }

    public class ServiceResult
    {
        public Exception? Exception { get; set; }
        public string? ExceptionMessage { get; set; }
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ServiceResult(bool isSuccess, HttpStatusCode statusCode, Exception? exception = null, string? exceptionMessage = null)
        {
            Exception = exception;
            ExceptionMessage = exceptionMessage;
            IsSuccess = isSuccess;
            StatusCode = statusCode;
        }

        [JsonConstructor]
        public ServiceResult() { }

        public static ServiceResult CreateSuccess()
        {
            return new ServiceResult(true, HttpStatusCode.OK);
        }

        public static ServiceResult CreateError(bool isSuccess, Exception ex, string? exceptionMessage = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult(false, statusCode, ex, exceptionMessage);
        }
    }
}
