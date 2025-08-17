using Trench.Notification.Application.Contracts.Messaging;
using Trench.Notification.Domain.Aggregates.Notification.Enums;

namespace Trench.Notification.Application.UseCases.Notifications.Commands.RegisterNotification;

public sealed record RegisterNotificationCommand(
    NotificationType Type,
    int RecipientUserId,
    int TriggeringUserId)
    : ICommand<bool>;