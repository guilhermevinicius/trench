using FluentValidation;

namespace Trench.User.Application.UseCases.User.Commands.Follower;

internal class FollowerCommandValidator : AbstractValidator<FollowerCommand>
{
    public FollowerCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.FollowerId)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);
    }
}