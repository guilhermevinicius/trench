using Trench.User.Domain.Aggregates.Users.Dtos;
using Entity = Trench.User.Domain.Aggregates.Users.Entities;

namespace Trench.User.Application.Contracts.Repositories;

public interface IUserRepository
{
    Task<bool> AlreadyUsernameExists(string username, CancellationToken cancellationToken);
    Task<bool> AlreadyEmailExists(string email, CancellationToken cancellationToken);
    Task InsertAsync(Entity.User user, CancellationToken cancellationToken);

    Task<GetUserLoggingDto?> GetUserLogging(string userId, CancellationToken cancellationToken);
}