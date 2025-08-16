using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trench.User.Application.Contracts.Caching;
using Trench.User.Application.Contracts.Data;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Domain.SeedWorks;
using Trench.User.Persistence.Caching;
using Trench.User.Persistence.Data;
using Trench.User.Persistence.Postgres;
using Trench.User.Persistence.Postgres.Repository;

namespace Trench.User.Persistence.Configurations;

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

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFollowerRepository, FollowerRepository>();
    }

    private static void ConfigureCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options => 
            options.Configuration = configuration.GetConnectionString("Redis"));

        services.AddSingleton<ICacheService, CacheService>();   
    }
}