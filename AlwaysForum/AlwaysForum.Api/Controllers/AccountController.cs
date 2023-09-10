using AlwaysForum.Api.Models.Api.Users;
using AlwaysForum.Api.Models.Dtos.Accounts;
using AlwaysForum.Api.Services.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace AlwaysForum.Api.Controllers;

public class AccountController : ApiController
{
    private readonly IAccountsService _accountsService;
    
    public AccountController(IAccountsService accountsService)
    {
        _accountsService = accountsService;
    }
    
    [HttpPost("login")]
    public async Task<LoginResponse> LoginAsync([FromBody] LoginDto loginDto, CancellationToken ct = default)
    {
        return await _accountsService.LoginAsync(loginDto, ct);
    }
    
    [HttpPost("register")]
    public async Task<RegisterResponse> RegisterAsync([FromBody] RegisterDto registerDto, CancellationToken ct = default)
    {
        return await _accountsService.RegisterAsync(registerDto, ct);
    }
}