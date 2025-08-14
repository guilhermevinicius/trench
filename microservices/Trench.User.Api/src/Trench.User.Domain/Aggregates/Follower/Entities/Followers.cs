using Trench.User.Domain.SeedWorks;

namespace Trench.User.Domain.Aggregates.Follower.Entities;

public class Followers : Entity
{
    public int FollowerId { get; private set; }
    public int FollowingId { get; private set; }
    public bool IsRequired { get; private set; }
    public bool Accepted { get; private set; }

    private Followers(int followerId, int followingId, bool isRequired)
    {
        FollowerId = followerId;
        FollowingId = followingId;
        IsRequired = isRequired;
    }

    private Followers()
    { }

    public static Followers Create(int followerId, int followingId, bool isRequired)
    {
        return new Followers(followerId, followingId, isRequired);
    }

    public void Accept()
    {
        Accepted = true;
    }

    public void Reject()
    {
        Accepted = false;
    }
}