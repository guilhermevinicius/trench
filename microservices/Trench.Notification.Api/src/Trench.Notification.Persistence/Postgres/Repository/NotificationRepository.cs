using Microsoft.EntityFrameworkCore;
using Trench.Notification.Application.Contracts.Repositories;
using Entity = Trench.Notification.Domain.Aggregates.Notification.Entities;

namespace Trench.Notification.Persistence.Postgres.Repository;

internal sealed class NotificationRepository(
    PostgresDbContext context)
    : INotificationRepository
{
    private readonly DbSet<Entity.Notification> _notifications = context.Set<Entity.Notification>();

    public async Task<Entity.Notification[]> ListNotificationsAsync(int recipientUserId, CancellationToken cancellationToken)
    {
        return await _notifications
            .AsNoTracking()
            .Where(x => x.RecipientUserId == recipientUserId)
            .OrderByDescending(x => x.CreatedOnUtc)
            .Take(30)
            .ToArrayAsync(cancellationToken);
    }

    public async Task InsertAsync(Entity.Notification notification, CancellationToken cancellationToken)
    {
        await context.AddAsync(notification, cancellationToken);
    }
}