using Microsoft.EntityFrameworkCore;
using Trench.User.Persistence.Postgres;

namespace Trench.User.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<PostgresDbContext>();
        dataContext.Database.Migrate();
    }
}