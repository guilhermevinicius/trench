using Trench.User.Application.Contracts.Messaging;

namespace Trench.User.Application.UseCases.User.Commands.Follower;

public sealed record FollowerCommand(
    string UserId,
    int FollowerId)
    : ICommand<bool>;