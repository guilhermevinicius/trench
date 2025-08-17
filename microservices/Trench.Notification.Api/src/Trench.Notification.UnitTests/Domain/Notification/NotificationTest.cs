using Trench.Notification.Domain.Aggregates.Notification.Enums;
using Trench.Notification.UnitTests.Config;
using Entity = Trench.Notification.Domain.Aggregates.Notification.Entities;

namespace Trench.Notification.UnitTests.Domain.Notification;

public class NotificationTest : BaseTest
{
    [Fact]
    public void Notification_Create_ShouldBeCreated()
    {
        // Arrange
        var type = Faker.Random.Enum<NotificationType>();
        var recipientUserId = Faker.Random.Int();
        var triggeringUserId = Faker.Random.Int();

        // Action
        var notification = Entity.Notification.Create(type, recipientUserId, triggeringUserId);

        // Assert
        Assert.Equal(type, notification.Type);
        Assert.Equal(recipientUserId, notification.RecipientUserId);
        Assert.Equal(triggeringUserId, notification.TriggeringUserId);
        Assert.False(notification.IsRead);
    }

    [Fact]
    public void Notification_MakeRead_ShouldBeMakeRead()
    {
        // Arrange
        var notification = GetNotificationScene();

        // Action
        notification.MakeRead();

        // Assert
        Assert.True(notification.IsRead);
    }

    #region Private Methods

    private Entity.Notification GetNotificationScene()
    {
        return Entity.Notification.Create(
            Faker.Random.Enum<NotificationType>(),
            Faker.Random.Int(),
            Faker.Random.Int());
    }

    #endregion
}