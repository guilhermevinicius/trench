using Entity = Trench.Notification.Domain.Aggregates.Notification.Entities;

namespace Trench.Notification.Application.Contracts.Repositories;

public interface INotificationRepository
{
    Task InsertAsync(Entity.Notification notification, CancellationToken cancellationToken);
}