using Trench.Notification.Application.UseCases.Notifications.Commands.RegisterNotification;
using Trench.Notification.Domain.Aggregates.Notification.Enums;
using Trench.Notification.UnitTests.Config;

namespace Trench.Notification.UnitTests.Application.Notification.Commands.RegisterNotification;

[Collection(nameof(RegisterNotificationAutoMockerCollection))]
public class RegisterNotificationTest(RegisterNotificationAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task RegisterNotification_Handler_ShouldBeCreatedSuccess()
    {
        // Arrange
        var command = new RegisterNotificationCommand(
            Faker.Random.Enum<NotificationType>(),
            Faker.Random.Int(),
            Faker.Random.Int());

        // Action
        var handler = fixture.GetInstance();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }
}