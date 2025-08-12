using Trench.User.Domain.Aggregates.Follower.Entities;
using Trench.User.UnitTests.Config;

namespace Trench.User.UnitTests.Domain.Aggregates.Follower;

public class FollowersTest : BaseTest
{
    [Fact]
    public void FollowersTest_Create_ShouldBeSuccess()
    {
        // Arrange
        var followerId = Faker.Random.Int();
        var followingId = Faker.Random.Int();
        var isRequired = Faker.Random.Bool();

        // Action
        var followers = Followers.Create(followerId, followingId, isRequired);

        // Assert
        Assert.Equal(followerId, followers.FollowerId);
        Assert.Equal(followingId, followers.FollowingId);
        Assert.Equal(isRequired, followers.IsRequired);
    }

    [Fact]
    public void FollowersTest_Create_Accepted()
    {
        // Arrange
        var followers = GetFollowerScene();

        // Action
        followers.Accept();

        // Assert
        Assert.True(followers.Accepted);
    }

    [Fact]
    public void FollowersTest_Create_Reject()
    {
        // Arrange
        var followers = GetFollowerScene();

        // Action
        followers.Accept();
        followers.Reject();

        // Assert
        Assert.False(followers.Accepted);
    }

    #region Private Methods

    private Followers GetFollowerScene()
    {
        return Followers.Create(
            Faker.Random.Int(),
        Faker.Random.Int(),
        Faker.Random.Bool());
    }

    #endregion
}