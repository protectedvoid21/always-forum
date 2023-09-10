using AlwaysForum.Api.Utils;

namespace AlwaysForum.Api.Models.Api.Users;

public class RegisterResponse : ResponseBase
{
    public string Token { get; set; } = null!;
}