using FluentValidation;

namespace Trench.User.Application.UseCases.User.Queries.GetUserByUsername;

internal class GetUserByUsernameQueryValidator : AbstractValidator<GetUserByUsernameQuery>
{
    public GetUserByUsernameQueryValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3);
    }
}