using FluentResults;
using Trench.Notification.Application.Contracts.Messaging;
using Trench.Notification.Application.Contracts.Repositories;
using Trench.Notification.Domain.SeedWorks;
using Entity = Trench.Notification.Domain.Aggregates.Notification.Entities;

namespace Trench.Notification.Application.UseCases.Notifications.Commands.RegisterNotification;

internal sealed class RegisterNotificationCommandHandler(
    INotificationRepository notificationRepository,
    IUnitOfWork unitOfWork) 
    : ICommandHandler<RegisterNotificationCommand, bool>
{
    public async Task<Result<bool>> Handle(RegisterNotificationCommand command, CancellationToken cancellationToken)
    {
        var notification = Entity.Notification.Create(
            command.Type,
            command.RecipientUserId,
            command.TriggeringUserId);

        await notificationRepository.InsertAsync(notification, cancellationToken);

        return await unitOfWork.CommitAsync(cancellationToken);
    }
}