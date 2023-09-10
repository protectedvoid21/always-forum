using FluentValidation;
using FluentValidation.Results;
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
    
    protected static TResponse NotFound<TResponse>() where TResponse : IResponse, new()
    {
        return CreateResponse<TResponse>(404, "Entity was not found");
    }
    
    protected static TResponse ValidationFailed<TResponse>(IEnumerable<ValidationFailure> errors) where TResponse : IResponse, new()
    {
        return CreateResponse<TResponse>(400, string.Join(" ", errors.Select(s => s.ErrorMessage)));
    }
    
    protected static TResponse HandleException<TResponse>(Exception ex) where TResponse : IResponse, new()
    {
        return CreateResponse<TResponse>(500, ex.Message);
    }
}