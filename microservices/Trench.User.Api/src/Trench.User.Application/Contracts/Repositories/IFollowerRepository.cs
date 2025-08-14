using Trench.User.Domain.Aggregates.Follower.Dtos;
using Trench.User.Domain.Aggregates.Follower.Entities;

namespace Trench.User.Application.Contracts.Repositories;

public interface IFollowerRepository
{
    Task<ListFollowersPendingDto[]> ListFollowersPending(int followingId, CancellationToken cancellationToken);
    Task<bool> AlreadyFollowerExists(int followerId, int followingId, CancellationToken cancellationToken);
    Task InsertAsync(Followers followers, CancellationToken cancellationToken);
}