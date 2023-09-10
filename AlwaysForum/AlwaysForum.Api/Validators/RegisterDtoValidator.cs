using AlwaysForum.Api.Models.Dtos.Accounts;
using FluentValidation;

namespace AlwaysForum.Api.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(18);
        
        RuleFor(x => x.Email)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();

        RuleFor(x => x.PasswordConfirmation)
            .Equal(x => x.Password)
            .NotEmpty();
    }
}