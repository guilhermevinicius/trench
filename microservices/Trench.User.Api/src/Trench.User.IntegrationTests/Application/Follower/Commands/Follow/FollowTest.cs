using Trench.User.Application.UseCases.Follower.Commands.Follow;
using Trench.User.Domain.Resources;
using Trench.User.IntegrationTests.Config;

namespace Trench.User.IntegrationTests.Application.Follower.Commands.Follow;

[Collection(nameof(IntegrationTestWebAppFactoryCollection))]
public class FollowTest(IntegrationTestWebAppFactory fixture) : BaseTest
{
    [Fact]
    public async Task Follow_Handler_ShouldBeFollowSuccess()
    {
        // Arrange
        var command = new FollowCommand(1, 1);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }

    [Fact]
    public async Task Follow_Handler_ShouldBeReturnErrorWhenAlreadyFollowerExists()
    {
        // Arrange
        var command = new FollowCommand(1, 2);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.AlreadyFollowerExists, result.Errors[0].Message);
    }
    
    [Fact]
    public async Task Follow_Handler_ShouldBeReturnErrorWhenFollowerNotFound()
    {
        // Arrange
        var command = new FollowCommand(1, 200);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.UserNotFound, result.Errors[0].Message);
    }
}