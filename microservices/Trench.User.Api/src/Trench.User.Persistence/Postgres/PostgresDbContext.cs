using Microsoft.EntityFrameworkCore;
using Pulse.Product.Domain.SeedWorks;

namespace Pulse.Product.Persistence.Postgres;

public class PostgresDbContext(
    DbContextOptions<PostgresDbContext> options)
    : DbContext(options), IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresDbContext).Assembly);
    }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken) > 0;
    }
}