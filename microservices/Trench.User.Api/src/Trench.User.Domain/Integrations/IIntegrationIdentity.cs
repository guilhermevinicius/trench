using Entity = Trench.User.Domain.Aggregates.Users.Entities;

namespace Trench.User.Domain.Integrations;

public interface IIntegrationIdentity
{
    Task<string?> RegisterAsync(Entity.User user, string password, CancellationToken cancellationToken);
}