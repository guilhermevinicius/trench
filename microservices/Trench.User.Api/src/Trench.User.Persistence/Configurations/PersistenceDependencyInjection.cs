using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Pulse.Product.Application.Contracts.Repositories;
using Pulse.Product.Application.Contracts.Repositories.Generics;
using Pulse.Product.Domain.SeedWorks;
using Pulse.Product.Persistence.MongoDb;
using Pulse.Product.Persistence.Postgres;
using Pulse.Product.Persistence.Postgres.Repository;

namespace Pulse.Product.Persistence.Configurations;

public static class PersistenceDependencyInjection
{
    public static IServiceCollection ConfigurePersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureMongoDb(configuration);
        services.ConfigurePostgres(configuration);

        return services;
    }

    private static void ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));

        services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoDbContext<>));

        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
    }

    private static void ConfigurePostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<PostgresDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("Postgres"))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PostgresDbContext>());

        services.AddScoped<IProductRepository, ProductRepository>();
    }
}