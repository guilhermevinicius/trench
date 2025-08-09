using Trench.User.Application.UseCases.User.Commands.Follower;
using Trench.User.Domain.Resources;
using Trench.User.IntegrationTests.Config;

namespace Trench.User.IntegrationTests.Application.User.Commands.Follower;

[Collection(nameof(IntegrationTestWebAppFactoryCollection))]
public class FollowerTest(IntegrationTestWebAppFactory fixture)
{
    [Fact]
    public async Task Follower_Handler_ShouldBeReturnFollowerSuccess()
    {
        // Arrange
        var command = new FollowerCommand("identityId",2);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }
    
    [Fact]
    public async Task Follower_Handler_ShouldBeReturnErrorWhenUserNotFound()
    {
        // Arrange
        var command = new FollowerCommand("identity-id-not-found",2);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.UserNotFound, result.Errors[0].Message);
    }

    [Fact]
    public async Task Follower_Handler_ShouldBeReturnErrorWhenFollowYourSelf()
    {
        // Arrange
        var command = new FollowerCommand("identityId",1);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.FollowYourself, result.Errors[0].Message);
    }

    [Fact]
    public async Task Follower_Handler_ShouldBeReturnErrorWhenFollowerNotFound()
    {
        // Arrange
        var command = new FollowerCommand("identityId",200);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.UserNotFound, result.Errors[0].Message);
    }
    
    [Fact]
    public async Task Follower_Handler_ShouldBeReturnErrorWhenAlreadyFollowerExists()
    {
        // Arrange
        var command = new FollowerCommand("identityId",2);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.AlreadyFollowerExists, result.Errors[0].Message);;
    }
}