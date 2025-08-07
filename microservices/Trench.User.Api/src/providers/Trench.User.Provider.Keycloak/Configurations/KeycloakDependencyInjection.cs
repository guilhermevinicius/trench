using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Trench.User.Domain.Integrations;
using Trench.User.Provider.Keycloak.Handlers;
using Trench.User.Provider.Keycloak.Models;

namespace Trench.User.Provider.Keycloak.Configurations;

public static class KeycloakDependencyInjection
{
    public static IServiceCollection ConfigureKeycloak(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<KeycloakOptions>(sp 
            => configuration.GetSection("Keycloak").Bind(sp));

        services.AddTransient<AdminAuthorizationDelegatingHandler>();

        services.AddHttpClient<IIntegrationIdentity, IntegrationIdentity>((serviceProvider, httpClient) =>
            {
                var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;
                httpClient.BaseAddress = new Uri($"{keycloakOptions.BaseUrl}/admin/realms/{keycloakOptions.Realm}/");
            })
            .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();

        services.AddHttpContextAccessor();

        return services;
    }
}