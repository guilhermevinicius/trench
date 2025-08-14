using Trench.User.Api.Configurations.Authentication;

namespace Trench.User.FunctionalTests.Config.Authentication;

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