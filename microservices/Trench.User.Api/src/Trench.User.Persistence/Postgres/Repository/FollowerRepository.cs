using Microsoft.EntityFrameworkCore;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Domain.Aggregates.Follower.Dtos;
using Trench.User.Domain.Aggregates.Follower.Entities;

namespace Trench.User.Persistence.Postgres.Repository;

internal sealed class FollowerRepository(
    PostgresDbContext context) 
    : IFollowerRepository
{
    private DbSet<Followers> _followers => context.Set<Followers>();

    public async Task<ListFollowersPendingDto[]> ListFollowersPending(int followingId,
        CancellationToken cancellationToken)
    {
        return await (from f in context.Set<Followers>().AsNoTracking()
            join user in context.Set<Domain.Aggregates.Users.Entities.User>() on f.FollowerId equals user.Id
            where f.FollowingId == followingId && !f.Accepted && f.IsRequired
            select new ListFollowersPendingDto(
                user.Id,
                user.PictureUrl,
                user.FirstName))
            .ToArrayAsync(cancellationToken);
    }

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