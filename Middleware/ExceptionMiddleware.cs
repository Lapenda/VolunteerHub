using System.Net;
using System.Text.Json;
using VolunteerHub.Results;

namespace VolunteerHub.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString(), "An unhandled error occured: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex) 
        {
            context.Response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;

            if (ex is UnauthorizedAccessException) statusCode = HttpStatusCode.Unauthorized;
            if (ex is KeyNotFoundException) statusCode = HttpStatusCode.NotFound;

            context.Response.StatusCode = (int)statusCode;

            var response = new ServiceResult(
                isSuccess: false,
                statusCode: statusCode,
                exceptionMessage: env.IsDevelopment() ? ex.Message : "A server error occured",
                exception: env.IsDevelopment() ? ex.ToString() : null 
            );

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }
}
