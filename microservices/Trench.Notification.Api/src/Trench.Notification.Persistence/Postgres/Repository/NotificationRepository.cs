using Trench.Notification.Application.Contracts.Repositories;
using Entity = Trench.Notification.Domain.Aggregates.Notification.Entities;

namespace Trench.Notification.Persistence.Postgres.Repository;

internal sealed class NotificationRepository(
    PostgresDbContext context)
    : INotificationRepository
{
    public async Task InsertAsync(Entity.Notification notification, CancellationToken cancellationToken)
    {
        await context.AddAsync(notification, cancellationToken);
    }
}