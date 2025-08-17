using Trench.Notification.Domain.Aggregates.Notification.Enums;
using Trench.Notification.Domain.SeedWorks;

namespace Trench.Notification.Domain.Aggregates.Notification.Entities;

public class Notification : Entity
{
    private Notification(NotificationType type, int recipientUserId, int triggeringUserId)
    {
        Type = type;
        RecipientUserId = recipientUserId;
        TriggeringUserId = triggeringUserId;
    }

    private Notification()
    {}

    public NotificationType Type { get; private set; }
    public int RecipientUserId { get; private set; }
    public int TriggeringUserId { get; private set; }
    public bool IsRead { get; private set; }

    public static Notification Create(NotificationType type, int recipientUserId, int triggeringUserId)
    {
        return new Notification(type, recipientUserId, triggeringUserId);
    }
    
    public void MakeRead()
    {
        IsRead = true;
    }
}