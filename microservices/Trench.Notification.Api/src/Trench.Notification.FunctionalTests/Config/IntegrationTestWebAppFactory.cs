using System.Text;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;
using Trench.Notification.Api.Configurations.Authentication;
using Trench.Notification.FunctionalTests.Config.Authentication;
using Trench.Notification.MessageQueue.Configurations;
using Trench.Notification.Persistence.Postgres;

namespace Trench.Notification.FunctionalTests.Config;

[CollectionDefinition(nameof(IntegrationTestWebAppFactoryCollection))]
public class IntegrationTestWebAppFactoryCollection : IClassFixture<IntegrationTestWebAppFactory>;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime, IDisposable
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:17.5")
        .Build();

    private readonly RabbitMqContainer _rabbitMqContainer = new RabbitMqBuilder()
        .Build();

    private HttpClient Client { get; set; } = new();
    public ISender Sender { get; private set; }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await _rabbitMqContainer.StartAsync();
        await InitializeClient();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _rabbitMqContainer.StopAsync();
    }

    public new void Dispose()
    {
        GC.SuppressFinalize(this);
        Client.Dispose();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureTestServices(services =>
        {
            var dbContextOptionsDescriptor = services.SingleOrDefault(descriptor =>
                descriptor.ServiceType == typeof(DbContextOptions<PostgresDbContext>)
            );

            services.Remove(dbContextOptionsDescriptor!);

            services.AddDbContextPool<PostgresDbContext>(options =>
                options.UseNpgsql(_dbContainer.GetConnectionString())
                    .UseSnakeCaseNamingConvention());

            var userContext = services.SingleOrDefault(descriptor => 
                descriptor.ServiceType == typeof(UserContext));

            services.Remove(userContext!);

            services.AddScoped<IUserContext, UserContextMock>();

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

    public async Task<HttpResponseMessage> SendRequest(HttpMethod httpMethod, string requestUri, string? jsonContent)
    {
        var request = new HttpRequestMessage(httpMethod, requestUri);

        if (!string.IsNullOrEmpty(jsonContent))
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        return await Client.SendAsync(request);
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

        await context.SaveChangesAsync();
    }
    #endregion
}