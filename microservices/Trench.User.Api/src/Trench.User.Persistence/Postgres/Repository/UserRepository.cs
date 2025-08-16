using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Trench.User.Application.Contracts.Caching;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Domain.Aggregates.Follower.Entities;
using Trench.User.Domain.Aggregates.Users.Dtos;
using Entity = Trench.User.Domain.Aggregates.Users.Entities;

namespace Trench.User.Persistence.Postgres.Repository;

internal sealed class UserRepository(
    ICacheService cache,
    PostgresDbContext context)
    : IUserRepository
{
    private readonly DbSet<Entity.User> _users = context.Set<Entity.User>();

    public async Task<bool> AlreadyUsernameExists(string username, CancellationToken cancellationToken)
    {
        return await _users
            .AsNoTracking()
            .AnyAsync(x => x.Username == username, cancellationToken);
    }

    public async Task<bool> AlreadyEmailExists(string email, CancellationToken cancellationToken)
    {
        return await _users
            .AsNoTracking()
            .AnyAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<Entity.User?> GetById(int userId, CancellationToken cancellationToken)
    {
        return await _users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
    }

    public async Task<GetUserLoggingDto?> GetUserLogging(string userId, CancellationToken cancellationToken)
    {
        return await _users
            .AsNoTracking()
            .Where(x => x.IdentityId == userId)
            .Select(user => new GetUserLoggingDto(
                user.FirstName,
                user.LastName,
                user.Username,
                user.Bio))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<GetUserByUsernameDto?> GetByUsername(string identityId, string username,
        CancellationToken cancellationToken)
    {
        return await _users
            .AsNoTracking()
            .Where(x => x.Username == username)
            .Select(user => new GetUserByUsernameDto(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Username,
                user.Bio,
                user.IsPublic,
                user.IdentityId == identityId,
                (from user2 in context.Set<Entity.User>()
                    join follower in context.Set<Followers>() on user2.Id equals follower.FollowerId
                    where user2.IdentityId == identityId && follower.FollowingId == user.Id && follower.IsRequired
                    select follower.Accepted == null
                ).FirstOrDefault(),
                (from user2 in context.Set<Entity.User>()
                    join follower in context.Set<Followers>() on user2.Id equals follower.FollowerId
                    where user2.IdentityId == identityId && follower.FollowingId == user.Id
                    select follower.Accepted == true
                ).FirstOrDefault()
            ))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task InsertAsync(Entity.User user, CancellationToken cancellationToken)
    {
        await _users.AddAsync(user, cancellationToken);
    }

    public async Task<Entity.User?> GetByIdentityId(string identityId, CancellationToken cancellationToken)
    {
        return await _users.FirstOrDefaultAsync(x => x.IdentityId == identityId, cancellationToken);
    }
}