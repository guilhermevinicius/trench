using Trench.Notification.Application.UseCases.Notifications.Queries.ListNotification;
using Trench.Notification.IntegrationTests.Config;
using Trench.User.IntegrationTests.Config;

namespace Trench.Notification.IntegrationTests.Application.Notification.Queries.ListNotification;

[Collection(nameof(IntegrationTestWebAppFactoryCollection))]
public class ListNotificationTest(IntegrationTestWebAppFactory fixture) : BaseTest
{
    [Fact]
    public async Task ListNotification_Handler_ShouldBeReturnNotifications()
    {
        // Arrange
        var query = new ListNotificationQuery(1);

        // Action
        var result = await fixture.Sender.Send(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEmpty(result.Value);
    }
    
    [Fact]
    public async Task ListNotification_Handler_ShouldBeReturnErrorWhenValidatorFailed()
    {
        // Arrange
        var query = new ListNotificationQuery(0);

        // Action
        var result = await fixture.Sender.Send(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task ListNotification_Handler_ShouldBeReturnEmptyWhenNotFoundNotifications()
    {
        // Arrange
        var query = new ListNotificationQuery(2);

        // Action
        var result = await fixture.Sender.Send(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Null(result.Value);
    }
}