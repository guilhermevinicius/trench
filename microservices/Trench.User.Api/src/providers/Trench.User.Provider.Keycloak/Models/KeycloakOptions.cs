namespace Trench.User.Provider.Keycloak.Models;

public sealed class KeycloakOptions
{
    public string BaseUrl { get; set; } = string.Empty;
    public string Realm { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string AdminClientId { get; set; } = string.Empty;
    public string AdminClientSecret { get; set; } = string.Empty;
    public string AuthClientId { get; set; } = string.Empty;
    public string AuthClientSecret { get; set; } = string.Empty;
}
