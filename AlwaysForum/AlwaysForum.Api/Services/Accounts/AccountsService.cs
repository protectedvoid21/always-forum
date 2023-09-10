using AlwaysForum.Api.Database;
using AlwaysForum.Api.Models.Api.Users;
using AlwaysForum.Api.Models.Dtos.Accounts;
using AlwaysForum.Api.Models.Models;
using AlwaysForum.Api.Services.Token;
using AlwaysForum.Api.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace AlwaysForum.Api.Services.Accounts;

public class AccountsService : ServiceBase, IAccountsService
{
    private readonly ForumDbContext _dbContext;
    private readonly ITokenService _tokenService;
    private readonly UserManager<ForumUser> _userManager;
    private readonly IValidator<LoginDto> _loginDtoValidator;
    private readonly IValidator<RegisterDto> _registerDtoValidator;

    public AccountsService(ForumDbContext dbContext,  
        ITokenService tokenService,
        UserManager<ForumUser> userManager,
        IValidator<LoginDto> loginDtoValidator, IValidator<RegisterDto> registerDtoValidator)
    {
        _dbContext = dbContext;
        _tokenService = tokenService;
        _userManager = userManager;
        _loginDtoValidator = loginDtoValidator;
        _registerDtoValidator = registerDtoValidator;
    }

    public async Task<LoginResponse> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
    {
        var validationResult = await _loginDtoValidator.ValidateAsync(loginDto, ct);
        if (!validationResult.IsValid)
        {
            return ValidationFailed<LoginResponse>(validationResult.Errors);
        }

        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            return BadRequest<LoginResponse>("Login failed. Incorrect email or password.");
        }

        var token = _tokenService.GenerateToken(user.Id, loginDto.Email);
        return Ok<LoginResponse>(response => response.Token = token);
    }
    
    public async Task<RegisterResponse> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
    {
        var validationResult = await _registerDtoValidator.ValidateAsync(registerDto, ct);
        if (!validationResult.IsValid)
        {
            return ValidationFailed<RegisterResponse>(validationResult.Errors);
        }

        var user = new ForumUser
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email
        };

        await _userManager.CreateAsync(user, registerDto.Password);

        var token = _tokenService.GenerateToken(user.Id, registerDto.Email);
        return Ok<RegisterResponse>(response => response.Token = token);
    }
}