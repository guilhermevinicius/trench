using Trench.User.Domain.Integrations;
using Entity = Trench.User.Domain.Aggregates.Users.Entities;

namespace Trench.User.FunctionalTests.Config.Integration;

public class IntegrationIdentityMock : IIntegrationIdentity
{
    public async Task<string?> RegisterAsync(Entity.User user, string password, CancellationToken cancellationToken)
    {
        return "jwt-access-token";
    }
}