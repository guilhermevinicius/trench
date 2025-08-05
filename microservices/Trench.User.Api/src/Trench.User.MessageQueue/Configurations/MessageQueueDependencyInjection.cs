using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Pulse.Product.MessageQueue.Configurations;

public static class MessageQueueDependencyInjection
{
    public static IServiceCollection ConfigureMessageQueue(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<QueueSettings>(configuration.GetSection(nameof(QueueSettings)));

        services.AddMassTransit(config =>
        {
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
            });
        });

        return services;
    }
}