using Trench.Notification.Api.Configurations;
using Trench.Notification.Api.Configurations.Observability;
using Trench.Notification.Application.Configurations;
using Trench.Notification.MessageQueue.Configurations;
using Trench.Notification.Persistence.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureOpenTelemetryLogger();

builder.Services
    .ConfigureApi(builder.Configuration)
    .ConfigureApplication()
    .ConfigurePersistence(builder.Configuration)
    .ConfigureMessageQueue(builder.Configuration);

var app = builder.Build();

await app.UseApi()
    .RunAsync();
    
public partial class Program;