using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trench.User.Domain.SeedWorks;
using Trench.User.Persistence.Postgres;

namespace Trench.User.Persistence.Configurations;

public static class PersistenceDependencyInjection
{
    public static IServiceCollection ConfigurePersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigurePostgres(configuration);

        return services;
    }

    private static void ConfigurePostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<PostgresDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("Postgres"))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PostgresDbContext>());
    }
}