using AlwaysForum.Api.Models.Dtos.Accounts;
using FluentValidation;

namespace AlwaysForum.Api.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}