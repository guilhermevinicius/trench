using Trench.Notification.Application.UseCases.Notifications.Commands.RegisterNotification;
using Trench.Notification.Domain.Aggregates.Notification.Enums;
using Trench.Notification.IntegrationTests.Config;
using Trench.User.IntegrationTests.Config;

namespace Trench.Notification.IntegrationTests.Application.Notification.Commands.RegisterNotification;

[Collection(nameof(IntegrationTestWebAppFactoryCollection))]
public class RegisterNotificationTest(IntegrationTestWebAppFactory fixture) : BaseTest
{
    [Fact]
    public async Task RegisterNotification_Handler_ShouldBeRegisterSuccess()
    {
        // Arrange
        var command = new RegisterNotificationCommand(
            NotificationType.FollowRequest,
            Faker.Random.Int(1),
            Faker.Random.Int(1));

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
        Assert.Empty(result.Errors);
    }
    
    [Fact]
    public async Task RegisterNotification_Handler_ShouldBeReturnErrorWhenValidatorFailed()
    {
        // Arrange
        var command = new RegisterNotificationCommand(
            NotificationType.FollowRequest,
            0,
            Faker.Random.Int(1));

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Errors);
    }
}