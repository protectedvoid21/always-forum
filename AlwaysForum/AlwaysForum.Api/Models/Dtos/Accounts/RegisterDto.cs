namespace AlwaysForum.Api.Models.Dtos.Accounts;

public class RegisterDto
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PasswordConfirmation { get; set; } = null!;
}