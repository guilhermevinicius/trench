namespace Trench.Notification.Api.Configurations.Authentication;

public interface IUserContext
{
    public string UserId();

    public int UserIdAsInt();
}