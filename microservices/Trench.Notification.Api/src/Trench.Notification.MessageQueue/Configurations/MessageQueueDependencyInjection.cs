using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Trench.Notification.MessageQueue.Consumers;

namespace Trench.Notification.MessageQueue.Configurations;

public static class MessageQueueDependencyInjection
{
    public static IServiceCollection ConfigureMessageQueue(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<HealthCheckPublisherOptions>(options =>
        {
            options.Delay = TimeSpan.FromSeconds(2);
            options.Predicate = check => check.Tags.Contains("ready");
        });

        services.Configure<QueueSettings>(configuration.GetSection(nameof(QueueSettings)));

        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            config.AddConsumersFromNamespaceContaining<RegisterNotificationConsumer>();

            config.UsingRabbitMq((context, cfg) =>
            {
                var queueSettings = context.GetRequiredService<IOptions<QueueSettings>>().Value;
                var host = new Uri($"{queueSettings.Host}:{queueSettings.Port}{queueSettings.VirtualHost}");

                cfg.Host(host, h =>
                {
                    h.Username(queueSettings.Username);
                    h.Password(queueSettings.Password);
                });

                cfg.ConfigureEndpoints(context);

                cfg.UseMessageRetry(x => 
                    x.Interval(3, TimeSpan.FromSeconds(30)));
            });

        });

        return services;
    }
}