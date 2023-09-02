using AlwaysForum.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AlwaysForum.Api.Filters;

public class ResponseBaseFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (resultContext.Result is ObjectResult objectResult && objectResult.Value is IResponse response)
        {
            objectResult.StatusCode = response.StatusCode;
            objectResult.Value = response;
        }
    }
}