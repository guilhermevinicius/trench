using FluentValidation;

namespace Trench.User.Application.UseCases.Follower.Commands.Follow;

internal sealed class FollowCommandValidator : AbstractValidator<FollowCommand>
{
    public FollowCommandValidator()
    {
        RuleFor(x => x.FollowerId)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.FollowingId)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);
    }
}