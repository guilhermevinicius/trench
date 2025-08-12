using Microsoft.EntityFrameworkCore;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Domain.Aggregates.Follower.Entities;

namespace Trench.User.Persistence.Postgres.Repository;

internal sealed class FollowerRepository(
    PostgresDbContext context) 
    : IFollowerRepository
{
    private DbSet<Followers> _followers => context.Set<Followers>();

    public async Task<bool> AlreadyFollowerExists(int followerId, int followingId, CancellationToken cancellationToken)
    {
        return await _followers
            .AsNoTracking()
            .AnyAsync(x => x.FollowerId == followerId && x.FollowingId == followingId, cancellationToken);
    }

    public async Task InsertAsync(Followers followers, CancellationToken cancellationToken)
    { 
        await _followers.AddAsync(followers, cancellationToken);
    }
}