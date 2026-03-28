using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VolunteerHub.Results;

namespace VolunteerHub.Filters
{
    public class ServiceResultFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var executedContext = await next();

            if (executedContext.Result is ObjectResult objectResult && objectResult.Value is IServiceResult serviceResult)
            {
                objectResult.StatusCode = (int)serviceResult.HttpStatusCode;
            }
        }
    }
}
