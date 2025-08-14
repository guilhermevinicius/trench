using Trench.User.Application.Contracts.Messaging;

namespace Trench.User.Application.UseCases.Follower.Commands.AcceptFollowerPending;

public sealed record AcceptFollowerPendingCommand(
    int FollowerId,
    int FollowingId,
    bool Accepted)
    : ICommand<bool>;