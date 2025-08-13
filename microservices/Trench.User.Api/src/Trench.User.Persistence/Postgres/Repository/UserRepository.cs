using Microsoft.EntityFrameworkCore;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Domain.Aggregates.Users.Dtos;
using Entity = Trench.User.Domain.Aggregates.Users.Entities;

namespace Trench.User.Persistence.Postgres.Repository;

internal sealed class UserRepository(
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

    public async Task<bool> AlreadyFollowerExists(int userId, int followerId, CancellationToken cancellationToken)
    {
        return await _users
            .AsNoTracking()
            .AnyAsync(x => x.Id == userId && x.Followers.Any(f => f.FollowerId == followerId), cancellationToken);
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
                user.Username))
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