using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;
using Testcontainers.Redis;
using Trench.User.Domain.Aggregates.Follower.Entities;
using Trench.User.Domain.Integrations;
using Trench.User.IntegrationTests.Config.Integration;
using Trench.User.MessageQueue.Configurations;
using Trench.User.Persistence.Postgres;
using Trench.User.Provider.Keycloak;
using Entity = Trench.User.Domain.Aggregates.Users.Entities;

namespace Trench.User.IntegrationTests.Config;

[CollectionDefinition(nameof(IntegrationTestWebAppFactoryCollection))]
public class IntegrationTestWebAppFactoryCollection : IClassFixture<IntegrationTestWebAppFactory>;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime, IDisposable
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:17.5")
        .Build();

    private readonly RabbitMqContainer _rabbitMqContainer = new RabbitMqBuilder()
        .Build();

    private readonly RedisContainer _redisContainer = new RedisBuilder()
        .Build();

    private HttpClient Client { get; set; } = new();
    public ISender Sender { get; private set; }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await _rabbitMqContainer.StartAsync();
        await _redisContainer.StartAsync();
        await InitializeClient();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _rabbitMqContainer.StopAsync();
        await _redisContainer.StopAsync();
    }

    public new void Dispose()
    {
        GC.SuppressFinalize(this);
        Client.Dispose();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var dbContextOptionsDescriptor = services.SingleOrDefault(descriptor =>
                descriptor.ServiceType == typeof(DbContextOptions<PostgresDbContext>)
            );

            services.Remove(dbContextOptionsDescriptor!);

            services.AddDbContextPool<PostgresDbContext>(options =>
                options.UseNpgsql(_dbContainer.GetConnectionString())
                    .UseSnakeCaseNamingConvention());

            var integrationIdentity = services.SingleOrDefault(descriptor => 
                descriptor.ServiceType == typeof(IntegrationIdentity));;

            services.Remove(integrationIdentity!);

            services.AddScoped<IIntegrationIdentity, IntegrationIdentityMock>();

            var settings = _rabbitMqContainer.GetConnectionString().Split(":");
            var login = settings[1].Replace("//", "");

            var queueSettings = new QueueSettings
            {
                Host = settings[2].Replace("@", "://"),
                Port = settings[3].Replace("/", ""),
                VirtualHost = "/",
                Username = login,
                Password = login
            };

            services.Configure<QueueSettings>(options =>
            {
                options.Host = queueSettings.Host;
                options.Port = queueSettings.Port;
                options.VirtualHost = queueSettings.VirtualHost;
                options.Username = queueSettings.Username;
                options.Password = queueSettings.Password;
            });

            var scope = services.BuildServiceProvider().CreateScope();
            Sender = scope.ServiceProvider.GetRequiredService<ISender>();
        });
    }

    #region Private Methods

    private async Task InitializeClient()
    {
        var clientOptions = new WebApplicationFactoryClientOptions();

        Client = CreateClient(clientOptions);

        await PopulateIntegrationTest();
    }

    private async Task PopulateIntegrationTest()
    {
        using var scope = Services.CreateScope();

        var provider = scope.ServiceProvider;

        await using var context = provider.GetRequiredService<PostgresDbContext>();

        await context.Database.EnsureCreatedAsync();

        await PopulateUser(context);

        await PopulateFollowers(context);

        await context.SaveChangesAsync();
    }

    private static async Task PopulateUser(PostgresDbContext context)
    {
        var user = Entity.User.Create(
            "Trench",
            "LTA",
            "trench@trench.com",
            "trench",
            DateTime.UtcNow);

        user.SetIdentityId("identityId");
        
        var user2 = Entity.User.Create(
            "Trench2",
            "LTA",
            "trench2@trench.com",
            "trench2",
            DateTime.UtcNow);

        user2.SetIdentityId("identityId2");

        await context.AddAsync(user, CancellationToken.None);
        await context.AddAsync(user2, CancellationToken.None);
    }

    private static async Task PopulateFollowers(PostgresDbContext context)
    {
        var followers = Followers.Create(1, 2, true);
        var followers2 = Followers.Create(2, 1, true);

        await context.AddAsync(followers, CancellationToken.None);
        await context.AddAsync(followers2, CancellationToken.None);
    }

    #endregion
}