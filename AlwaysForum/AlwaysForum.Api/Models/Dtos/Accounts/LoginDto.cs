namespace AlwaysForum.Api.Models.Dtos.Accounts;

public class LoginDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}