using Trench.Notification.Domain.Aggregates.Notification.Enums;
using Trench.Notification.Domain.Dtos;

namespace Trench.Notification.Domain.Aggregates.Notification.Dtos;

public sealed record NotificationDto(
    int Id,
    NotificationType Type,
    bool IsRead,
    string Message,
    UserProfileDto? RecipientProfile,
    DateTime OccurredOn);