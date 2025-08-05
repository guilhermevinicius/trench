using Pulse.Product.Api.Configurations.Endpoints;
using Pulse.Product.Api.Configurations.ExceptionHandler;
using Pulse.Product.Api.Configurations.HealthCheck;
using Pulse.Product.Api.Configurations.Observability;
using Pulse.Product.Api.Extensions;
using Scalar.AspNetCore;
using Serilog;

namespace Pulse.Product.Api.Configurations;

internal static class ApiDependencyInjection
{
    internal static WebApplication UseApi(this WebApplication app)
    {
        app.MapOpenApi();

        app.MapScalarApiReference();

        app.UseHealthCheckConfiguration();

        app.AddOpenTelemetryConfig();

        app.UseCors(x
            => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        // app.UseRequestContextLogging();

        app.UseSerilogRequestLogging();
        
        app.ApplyMigrations();

        app.MapEndpoints();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseExceptionHandler();

        return app;
    }

    internal static IServiceCollection ConfigureApi(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOpenApi();

        services.AddEndpoints();

        services.AddHealthCheckConfiguration(configuration);

        services.AddCors();

        services.AddAuthentication();

        services.AddAuthorization();

        services.ConfigureOpenTelemetry();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddProblemDetails();

        return services;
    }
}