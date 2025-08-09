using Trench.User.Domain.SeedWorks;

namespace Trench.User.Domain.Aggregates.Users.Entities;

public class UserFollower : Entity
{
    public User User { get; private set; }
    public int UserId { get; private set; }
    public User Follower { get; private set; }
    public int FollowerId { get; private set; }

    private UserFollower(int userId, int followerId)
    {
        UserId = userId;
        FollowerId = followerId;
    }

    private UserFollower()
    {}

    public static UserFollower Create(int userId, int followerId)
    {
        return new UserFollower(userId, followerId);
    }
}