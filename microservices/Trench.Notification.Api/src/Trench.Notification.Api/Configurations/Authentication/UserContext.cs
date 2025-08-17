using System.Security.Claims;

namespace Trench.Notification.Api.Configurations.Authentication;

public class UserContext(
    IHttpContextAccessor contextAccessor)
    : IUserContext
{
    public string UserId()
    {
        return contextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
            ?.Value!;
    }

    public int UserIdAsInt()
    {
        return 1;
    }
}