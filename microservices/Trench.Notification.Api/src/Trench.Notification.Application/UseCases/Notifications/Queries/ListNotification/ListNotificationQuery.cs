using Trench.Notification.Application.Contracts.Messaging;
using Trench.Notification.Domain.Aggregates.Notification.Dtos;

namespace Trench.Notification.Application.UseCases.Notifications.Queries.ListNotification;

public sealed record ListNotificationQuery(
    int RecipientUserId)
    : IQuery<NotificationDto[]>;