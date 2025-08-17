using Microsoft.EntityFrameworkCore;
using Trench.Notification.Domain.SeedWorks;

namespace Trench.Notification.Persistence.Postgres;

public class PostgresDbContext(
    DbContextOptions<PostgresDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public async Task<bool> CommitAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken) > 0;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresDbContext).Assembly);
    }
}