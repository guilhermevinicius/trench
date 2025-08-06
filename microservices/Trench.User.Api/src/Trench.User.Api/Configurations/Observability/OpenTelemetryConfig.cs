using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

namespace Trench.User.Api.Configurations.Observability;

internal static class OpenTelemetryConfig
{
    internal static WebApplicationBuilder ConfigureOpenTelemetryLogger(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(context.Configuration));

        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
            logging.AddOtlpExporter(opt => { opt.Endpoint = new Uri("http://localhost:4319"); });
        });

        return builder;
    }

    internal static IServiceCollection ConfigureOpenTelemetry(this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("Pulse.Product.Api"))
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation();
                metrics.AddHttpClientInstrumentation();
                metrics.AddMeter("Pulse.Product.Api");
                metrics.AddMeter("Microsoft.AspNetCore.Hosting");
                metrics.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
                metrics.AddPrometheusExporter();
            })
            .WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation();
                tracing.AddHttpClientInstrumentation();
                tracing.AddSource("Pulse.Product.Api");
                tracing.AddEntityFrameworkCoreInstrumentation(options => { options.SetDbStatementForText = true; });
                tracing.AddOtlpExporter(opt =>
                {
                    opt.Endpoint = new Uri("http://localhost:4317");
                    opt.Protocol = OtlpExportProtocol.Grpc;
                });
            })
            .WithLogging(logging => { logging.AddOtlpExporter(); });

        return services;
    }

    internal static IApplicationBuilder AddOpenTelemetryConfig(this IApplicationBuilder app)
    {
        app.UseOpenTelemetryPrometheusScrapingEndpoint();

        return app;
    }
}