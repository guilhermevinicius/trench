using Trench.Notification.Api.Configurations.Authentication;

namespace Trench.Notification.FunctionalTests.Config.Authentication;

internal class UserContextMock : IUserContext
{
    public string UserId()
    {
        return "identityId";
    }

    public int UserIdAsInt()
    {
        return 1;
    }
}