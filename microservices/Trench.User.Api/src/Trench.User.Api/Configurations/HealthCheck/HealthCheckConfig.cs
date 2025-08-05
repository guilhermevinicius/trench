using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;
using Pulse.Product.Persistence.Configurations;

namespace Pulse.Product.Api.Configurations.HealthCheck;

internal static class HealthCheckConfig
{
    internal static void AddHealthCheckConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        var mongoConnectionString = configuration
            .GetSection(nameof(MongoDbSettings))
            .Get<MongoDbSettings>()
            .GetConnectionString();
        
        var settings = MongoClientSettings.FromConnectionString(mongoConnectionString);
        settings.SslSettings = new SslSettings
        {
            EnabledSslProtocols = System.Security.Authentication.SslProtocols.None
        };

        services.AddSingleton<IMongoClient>(_ => 
            new MongoClient(settings));

        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Postgres"), name: "Postgres")
            .AddMongoDb(
                clientFactory: sp => sp.GetRequiredService<IMongoClient>(),
                name: "MongoDB",
                failureStatus: HealthStatus.Unhealthy);

        services
            .AddHealthChecksUI(setup => { setup.SetEvaluationTimeInSeconds(50); })
            .AddInMemoryStorage();
    }

    internal static void UseHealthCheckConfiguration(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks("/_healthcheck", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        endpoints.MapHealthChecksUI(setup => { setup.UIPath = "/_healthcheck-ui"; });
    }
}