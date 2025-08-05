using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pulse.Product.CrossCutting.Behaviors;
using Pulse.Product.CrossCutting.GlobalException;

namespace Pulse.Product.Application.Configurations;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(ApplicationDependencyInjection).Assembly);

            config.AddOpenBehavior(typeof(LoggingBehavior<,>));

            config.AddOpenBehavior(typeof(ValidatorPipelineBehavior<,>));

            config.AddOpenBehavior(typeof(GlobalExceptionPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(ApplicationDependencyInjection).Assembly);

        return services;
    }
}