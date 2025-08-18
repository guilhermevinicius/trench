using Moq;
using Trench.Notification.Application.Contracts.Caching;
using Trench.Notification.Application.Contracts.Repositories;
using Trench.Notification.Application.UseCases.Notifications.Queries.ListNotification;
using Trench.Notification.Domain.Dtos;
using Trench.Notification.UnitTests.Config;

namespace Trench.Notification.UnitTests.Application.Notification.Queries.ListNotification;

[Collection(nameof(ListNotificationAutoMockerCollection))]
public class ListNotificationTest(ListNotificationAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task ListNotification_Handler_ShouldBeCreatedSuccess()
    {
        // Arrange
        var query = new ListNotificationQuery(Faker.Random.Int());

        // Action
        var handler = fixture.GetInstance();
        fixture.MockListNotification();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEmpty(result.Value);
        fixture.AutoMocker.Verify<INotificationRepository>(x =>
            x.ListNotificationsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        fixture.AutoMocker.Verify<ICacheService>(x =>
            x.GetAsync<UserProfileDto>(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
    }

    [Fact]
    public async Task ListNotification_Handler_ShouldBeReturnEmptyNotifications()
    {
        // Arrange
        var query = new ListNotificationQuery(Faker.Random.Int());

        // Action
        var handler = fixture.GetInstance();
        fixture.MockListNotificationEmpty();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Null(result.Value);
        fixture.AutoMocker.Verify<INotificationRepository>(x =>
            x.ListNotificationsAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        fixture.AutoMocker.Verify<ICacheService>(x =>
            x.GetAsync<UserProfileDto>(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}