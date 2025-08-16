using Scalar.AspNetCore;
using Serilog;
using Trench.Notification.Api.Configurations.Authentication;
using Trench.Notification.Api.Configurations.ExceptionHandler;
using Trench.Notification.Api.Configurations.HealthCheck;
using Trench.Notification.Api.Configurations.Observability;
using Trench.Notification.Api.Extensions;

namespace Trench.Notification.Api.Configurations;

internal static class ApiDependencyInjection
{
    internal static IServiceCollection ConfigureApi(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddGrpc();

        services.AddOpenApi();

        services.AddControllers();

        services.AddHealthCheckConfiguration(configuration);

        services.AddCors();

        services.ConfigureAuthentication(configuration);

        services.AddAuthorization();

        services.ConfigureOpenTelemetry();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddProblemDetails();

        return services;
    }

    internal static WebApplication UseApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHealthCheckConfiguration();

        app.AddOpenTelemetryConfig();

        app.UseCors(x
            => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        // app.UseRequestContextLogging();

        app.UseSerilogRequestLogging();

        app.ApplyMigrations();

        if (app.Environment.IsEnvironment("Test"))
            app.MapControllers().AllowAnonymous();
        else
            app.MapControllers();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseExceptionHandler();

        return app;
    }
}