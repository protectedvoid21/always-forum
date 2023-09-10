using AlwaysForum.Api.Utils;

namespace AlwaysForum.Api.Models.Api.Users;

public class LoginResponse : ResponseBase
{
    public string Token { get; set; } = null!;
}