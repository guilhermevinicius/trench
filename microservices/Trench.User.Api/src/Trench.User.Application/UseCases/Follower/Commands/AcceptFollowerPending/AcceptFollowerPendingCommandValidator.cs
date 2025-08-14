using FluentValidation;

namespace Trench.User.Application.UseCases.Follower.Commands.AcceptFollowerPending;

internal class AcceptFollowerPendingCommandValidator : AbstractValidator<AcceptFollowerPendingCommand>
{
    public AcceptFollowerPendingCommandValidator()
    {
        RuleFor(x => x.FollowerId)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.FollowingId)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Accepted);
    }
}