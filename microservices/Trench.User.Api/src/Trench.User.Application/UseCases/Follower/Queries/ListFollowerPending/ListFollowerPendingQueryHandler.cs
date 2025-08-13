using FluentResults;
using Trench.User.Application.Contracts.Messaging;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Domain.Aggregates.Follower.Dtos;

namespace Trench.User.Application.UseCases.Follower.Queries.ListFollowerPending;

internal sealed class ListFollowerPendingQueryHandler(
    IFollowerRepository followerRepository) 
    : IQueryHandler<ListFollowerPendingQuery, ListFollowersPendingDto[]>
{
    public async Task<Result<ListFollowersPendingDto[]>> Handle(ListFollowerPendingQuery query,
        CancellationToken cancellationToken)
    {
        return await followerRepository.ListFollowersPending(query.FollowerId, cancellationToken);
    }
}