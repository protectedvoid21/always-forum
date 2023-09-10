namespace AlwaysForum.Api.Services.Token;

public interface ITokenService
{
    string GenerateToken(string userId, string email);
}