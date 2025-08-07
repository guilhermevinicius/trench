using Trench.User.Api.Configurations;
using Trench.User.Api.Configurations.Observability;
using Trench.User.Application.Configurations;
using Trench.User.MessageQueue.Configurations;
using Trench.User.Persistence.Configurations;
using Trench.User.Provider.Keycloak.Configurations;
using Trench.User.Storage.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureOpenTelemetryLogger();

builder.Services
    .ConfigureApi(builder.Configuration)
    .ConfigureApplication()
    .ConfigurePersistence(builder.Configuration)
    .ConfigureMessageQueue(builder.Configuration)
    .ConfigureStorage(builder.Configuration)
    .ConfigureKeycloak(builder.Configuration);

var app = builder.Build();

await app
    .UseApi()
    .RunAsync();
    
public partial class Program;