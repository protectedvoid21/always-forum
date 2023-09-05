using FluentValidation;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace AlwaysForum.Api.Utils;

public class ServiceBase
{
    protected static TResponse CreateResponse<TResponse>(int statusCode, string? errorMessage = null)
        where TResponse : IResponse, new()
    {
        return new TResponse
        {
            StatusCode = statusCode,
            ErrorMessage = errorMessage
        };
    }
    
    protected static TResponse Ok<TResponse>(Action<TResponse>? customizeResponse = null) where TResponse : IResponse, new()
    {
        var response = CreateResponse<TResponse>(200);
        customizeResponse?.Invoke(response);
        return response;
    }
    
    protected static TResponse BadRequest<TResponse>(string errorMessage) where TResponse : IResponse, new()
    {
        return CreateResponse<TResponse>(400, errorMessage);
    }
    
    protected static TResponse NotFound<TResponse>(string errorMessage) where TResponse : IResponse, new()
    {
        return CreateResponse<TResponse>(404, errorMessage);
    }

    protected static TResponse InternalServerError<TResponse>(string errorMessage) where TResponse : IResponse, new()
    {
        return CreateResponse<TResponse>(500, errorMessage);
    }
    
    protected static TResponse HandleValidationException<TResponse>(ValidationException ex) where TResponse : IResponse, new()
    {
        return CreateResponse<TResponse>(400, ex.Message);
    }
    
    protected static TResponse HandleException<TResponse>(Exception ex) where TResponse : IResponse, new()
    {
        return CreateResponse<TResponse>(500, ex.Message);
    }
}