using Trench.User.Application.Contracts.Messaging;

namespace Trench.User.Application.UseCases.Follower.Commands.Follow;

public sealed record FollowCommand(
    int FollowerId,
    int FollowingId)
    : ICommand<bool>;