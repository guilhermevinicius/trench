using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Trench.User.CrossCutting.Behaviors;
using Trench.User.CrossCutting.GlobalException;

namespace Trench.User.Application.Configurations;

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