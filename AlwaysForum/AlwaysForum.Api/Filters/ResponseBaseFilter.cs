using System.Net;
using AlwaysForum.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AlwaysForum.Api.Filters;

public class ResponseBaseFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        try
        {
            var resultContext = await next();

            if (resultContext.Result is ObjectResult result && result.Value is IResponse response)
            {
                result.StatusCode = response.StatusCode;
                result.Value = response;   
            }
        }
        catch(Exception ex)
        {
            await HandleExceptionAsync(context.HttpContext, ex);
        }
    }
    
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsJsonAsync(new ResponseBase
        {
            StatusCode = context.Response.StatusCode,
            ErrorMessage = "Internal Server Error from the custom middleware."
        });
    }
}