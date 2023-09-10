using AlwaysForum.Api.Models.Api.Posts;
using FluentValidation;

namespace AlwaysForum.Api.Validators;

public class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>
{
    public CreatePostRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleFor(x => x.SectionId)
            .GreaterThanOrEqualTo(0);
    }
}