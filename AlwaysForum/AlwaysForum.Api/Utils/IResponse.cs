namespace AlwaysForum.Api.Utils;

public interface IResponse
{
    int StatusCode { get; set; }
    string? ErrorMessage { get; set; }
}