using Trench.User.Application.Contracts.Messaging;
using Trench.User.Domain.Aggregates.Follower.Dtos;

namespace Trench.User.Application.UseCases.Follower.Queries.ListFollowerPending;

public sealed record ListFollowerPendingQuery(
    int FollowerId)
    : IQuery<ListFollowersPendingDto[]>;