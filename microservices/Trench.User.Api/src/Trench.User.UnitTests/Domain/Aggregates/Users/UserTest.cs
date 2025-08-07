using Trench.User.UnitTests.Config;
using Entity = Trench.User.Domain.Aggregates.Users.Entities;

namespace Trench.User.UnitTests.Domain.Aggregates.Users;

public class UserTest : BaseTest
{
    [Fact]
    public void UserTest_Create()
    {
        // Arrange
        var firstName = Faker.Name.FirstName();
        var lastName = Faker.Name.LastName();
        var email = Faker.Internet.Email();
        var username = Faker.Internet.UserName();
        var birthDate = Faker.Date.Past(18);

        // Action
        var user = Entity.User.Create(firstName, lastName, email, username, birthDate);

        // Assert
        Assert.Equal(firstName, user.FirstName);
        Assert.Equal(lastName, user.LastName);
        Assert.Equal(email, user.Email);
        Assert.Equal(birthDate, user.Birthdate);
        Assert.Equal(username, user.Username);
        Assert.True(user.IsActive);
    }

    [Fact]
    public void UserTest_Create_UpdateBio()
    {
        // Arrange
        var user = GetUserScene();
        var bio = Faker.Lorem.Paragraph();

        // Action
        user.UpdateBio(bio);

        // Assert
        Assert.Equal(bio, user.Bio);
    }

    [Fact]
    public void UserTest_Create_UpdatePictureUrl()
    {
        // Arrange
        var user = GetUserScene();
        var url = Faker.Random.AlphaNumeric(10);

        // Action
        user.UpdatePictureUrl(url);

        // Assert
        Assert.Equal(url, user.PictureUrl);
    }

    [Fact]
    public void UserTest_Create_SetIdentityId()
    {
        // Arrange
        var user = GetUserScene();
        var identityId = Faker.Random.Guid().ToString();

        // Action
        user.SetIdentityId(identityId);

        // Assert
        Assert.Equal(identityId, user.IdentityId);
    }


    [Fact]
    public void UserTest_Create_Activate()
    {
        // Arrange
        var user = GetUserScene();

        // Action
        user.Deactivate();
        user.Activate();

        // Assert
        Assert.True(user.IsActive);
    }
    
    [Fact]
    public void UserTest_Create_Deactivate()
    {
        // Arrange
        var user = GetUserScene();

        // Action
        user.Deactivate();

        // Assert
        Assert.False(user.IsActive);
    }

    
    #region Private Methods

    private Entity.User GetUserScene()
    {
        return Entity.User.Create(
            Faker.Name.FirstName(),
            Faker.Name.LastName(),
            Faker.Internet.Email(),
            Faker.Internet.UserName(),
        Faker.Date.Past(18));
    }

    #endregion
    
}