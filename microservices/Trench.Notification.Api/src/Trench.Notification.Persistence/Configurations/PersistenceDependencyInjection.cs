using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trench.Notification.Application.Contracts.Caching;
using Trench.Notification.Application.Contracts.Data;
using Trench.Notification.Domain.SeedWorks;
using Trench.Notification.Persistence.Caching;
using Trench.Notification.Persistence.Data;
using Trench.Notification.Persistence.Postgres;

namespace Trench.Notification.Persistence.Configurations;

public static class PersistenceDependencyInjection
{
    public static IServiceCollection ConfigurePersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigurePostgres(configuration);
        services.ConfigureCache(configuration);

        return services;
    }

    private static void ConfigurePostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres")
            ?? throw new ArgumentNullException(nameof(configuration));;

        services.AddDbContextPool<PostgresDbContext>(opt =>
            opt.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention());

        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PostgresDbContext>());
    }

    private static void ConfigureCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options => 
            options.Configuration = configuration.GetConnectionString("Redis"));

        services.AddSingleton<ICacheService, CacheService>();   
    }
}