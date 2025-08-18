using Moq;
using Moq.AutoMock;
using Trench.Notification.Application.Contracts.Repositories;
using Trench.Notification.Application.UseCases.Notifications.Queries.ListNotification;
using Trench.Notification.Domain.Aggregates.Notification.Enums;
using Trench.Notification.UnitTests.Config;
using Entity = Trench.Notification.Domain.Aggregates.Notification.Entities;

namespace Trench.Notification.UnitTests.Application.Notification.Queries.ListNotification;

[CollectionDefinition(nameof(ListNotificationAutoMockerCollection))]
public class ListNotificationAutoMockerCollection : IClassFixture<ListNotificationAutoMockerFixture>;

public class ListNotificationAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new();

    internal ListNotificationQueryHandler GetInstance()
    {
        AutoMocker = new AutoMocker();
        return AutoMocker.CreateInstance<ListNotificationQueryHandler>();
    }

    public void MockListNotification()
    {
        AutoMocker.GetMock<INotificationRepository>()
            .Setup(x => x.ListNotificationsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([
                GetNotificationsScene(),
                GetNotificationsScene()
            ]);
    }
    
    public void MockListNotificationEmpty()
    {
        AutoMocker.GetMock<INotificationRepository>()
            .Setup(x => x.ListNotificationsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);
    }

    #region Private Methods

    private Entity.Notification GetNotificationsScene()
    {
        return Entity.Notification.Create(
            NotificationType.FollowRequest,
            Faker.Random.Int(),
            Faker.Random.Int());
    }

    #endregion
    
}