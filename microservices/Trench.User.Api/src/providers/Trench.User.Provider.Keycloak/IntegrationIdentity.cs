using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Trench.User.Domain.Integrations;
using Trench.User.Domain.Integrations.Dtos;
using Trench.User.Provider.Keycloak.Models;

namespace Trench.User.Provider.Keycloak;

internal sealed class IntegrationIdentity(
    HttpClient httpClient) 
    : IIntegrationIdentity
{
    private const string PasswordCredentialType = "password";

    public async Task<string?> RegisterAsync(Domain.Aggregates.Users.Entities.User user, string password, CancellationToken cancellationToken)
    {
        var userRepresentationModel = UserRepresentationModel.FromUser(user);

        userRepresentationModel.Credentials =
        [
            new CredentialRepresentationModel
            {
                Value = password,
                Temporary = false,
                Type = PasswordCredentialType
            }
        ];

        var response = await httpClient.PostAsJsonAsync(
            "users",
            userRepresentationModel,
            cancellationToken);

        return ExtractIdentityIdFromLocationHeader(response);
    }

    #region Private methods

    private static string ExtractIdentityIdFromLocationHeader(
        HttpResponseMessage httpResponseMessage)
    {
        const string usersSegmentName = "users/";

        var locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery ??
                             throw new InvalidOperationException("Location header can't be null");

        var userSegmentValueIndex = locationHeader.IndexOf(
            usersSegmentName,
            StringComparison.InvariantCultureIgnoreCase);

        var userIdentityId = locationHeader[(userSegmentValueIndex + usersSegmentName.Length)..];

        return userIdentityId;
    }

    #endregion
}
