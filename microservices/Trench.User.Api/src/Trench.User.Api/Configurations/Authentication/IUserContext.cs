namespace Trench.User.Api.Configurations.Authentication;

public interface IUserContext
{
    public string UserId();

    public int UserIdAsInt();
}