using FluentValidation;

namespace Trench.User.Application.UseCases.User.Queries.GetUserLogging;

internal class GetUserLoggingQueryValidator : AbstractValidator<GetUserLoggingQuery>
{
    public GetUserLoggingQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .NotNull();
    }
}