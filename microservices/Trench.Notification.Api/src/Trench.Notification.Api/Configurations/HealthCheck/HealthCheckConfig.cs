using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Trench.Notification.Api.Configurations.HealthCheck;

internal static class HealthCheckConfig
{
    internal static void AddHealthCheckConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Postgres")!, name: "Postgres")
            .AddRedis(configuration.GetConnectionString("Redis")!, "Redis");

        services.AddHealthChecksUI(setup => { setup.SetEvaluationTimeInSeconds(50); })
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