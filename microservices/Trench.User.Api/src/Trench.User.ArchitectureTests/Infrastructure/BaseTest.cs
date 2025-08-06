using System.Reflection;
using Trench.User.Application.Contracts.Messaging;
using Trench.User.Domain.SeedWorks;
using Trench.User.Persistence.Postgres;
using Trench.User.Storage.Configurations;

namespace Trench.User.ArchitectureTests.Infrastructure;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(IBaseCommand).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;

    protected static readonly Assembly PersistenceAssembly = typeof(PostgresDbContext).Assembly;

    protected static readonly Assembly StorageAssembly = typeof(StorageDependencyInjection).Assembly;
}
