using Microsoft.EntityFrameworkCore;
using Trench.Notification.Persistence.Postgres;

namespace Trench.Notification.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<PostgresDbContext>();
        dataContext.Database.Migrate();
    }
}