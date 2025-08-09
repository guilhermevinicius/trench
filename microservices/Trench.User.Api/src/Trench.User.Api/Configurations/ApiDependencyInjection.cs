using Scalar.AspNetCore;
using Serilog;
using Trench.User.Api.Configurations.Authentication;
using Trench.User.Api.Configurations.Endpoints;
using Trench.User.Api.Configurations.ExceptionHandler;
using Trench.User.Api.Configurations.HealthCheck;
using Trench.User.Api.Configurations.Observability;
using Trench.User.Api.Extensions;

namespace Trench.User.Api.Configurations;

internal static class ApiDependencyInjection
{
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

        services.ConfigureAuthentication(configuration);

        services.AddAuthorization();

        services.ConfigureOpenTelemetry();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddProblemDetails();

        return services;
    }
}