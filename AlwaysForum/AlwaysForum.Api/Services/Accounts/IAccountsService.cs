using AlwaysForum.Api.Models.Api.Users;
using AlwaysForum.Api.Models.Dtos.Accounts;

namespace AlwaysForum.Api.Services.Accounts;

public interface IAccountsService
{
    Task<LoginResponse> LoginAsync(LoginDto loginDto, CancellationToken ct = default);

    Task<RegisterResponse> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default);
}