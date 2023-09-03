using System.Net;

namespace AlwaysForum.Api.Utils;

public abstract class ResponseBase : IResponse
{
    public int StatusCode { get; set; }
    public bool IsSuccess => ErrorMessage is null;
    public string? ErrorMessage { get; set; }
}