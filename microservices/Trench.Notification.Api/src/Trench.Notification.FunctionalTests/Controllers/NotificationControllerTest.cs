using System.Collections;
using Trench.Notification.FunctionalTests.Config;
using Trench.Notification.FunctionalTests.Config.Helpers;

namespace Trench.Notification.FunctionalTests.Controllers;

[Collection(nameof(IntegrationTestWebAppFactoryCollection))]
public class NotificationControllerTest(IntegrationTestWebAppFactory fixture) : BaseTest
{
    [Fact]
    public async Task NotificationController_Get_ShouldBeReturnNotifications()
    {
        // Arrange
        const string uri = "/api/v1/notifications";

        // Action
        var responseMessage = await fixture.SendRequest(HttpMethod.Get, uri, null);
        var response = await JsonHelper.DeserializeResponse(responseMessage);

        // Assert
        Assert.Equal(200, response?.StatusCode);
    }
}