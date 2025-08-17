using System.Reflection;
using Trench.Notification.Application.Contracts.Messaging;
using Trench.Notification.Domain.SeedWorks;
using Trench.Notification.Persistence.Postgres;

namespace Trench.Notification.ArchitectureTests.Infrastructure;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(IBaseCommand).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;

    protected static readonly Assembly PersistenceAssembly = typeof(PostgresDbContext).Assembly;
}