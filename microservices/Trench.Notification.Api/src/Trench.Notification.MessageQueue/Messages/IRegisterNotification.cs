using Trench.Notification.Domain.Aggregates.Notification.Enums;

namespace Trench.Notification.MessageQueue.Messages;

public interface IRegisterNotification
{
    public NotificationType Type { get; }
    public int RecipientUserId { get; }
    public int TriggeringUserId { get; }
}