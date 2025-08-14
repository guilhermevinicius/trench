using Trench.User.Application.UseCases.Follower.Commands.AcceptFollowerPending;
using Trench.User.Domain.Resources;
using Trench.User.IntegrationTests.Config;

namespace Trench.User.IntegrationTests.Application.Follower.Commands.AcceptFollowerPending;

[Collection(nameof(IntegrationTestWebAppFactoryCollection))]
public class AcceptFollowerPendingTest(IntegrationTestWebAppFactory fixture) : BaseTest
{
    [Fact]
    public async Task AcceptFollowerPending_Handler_ShouldBeAcceptFollowerPendingSuccess()
    {
        // Arrange
        var command = new AcceptFollowerPendingCommand(
            1,
            2,
            true);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }

    [Fact]
    public async Task AcceptFollowerPending_Handler_ShouldBeRejectFollowerPendingSuccess()
    {
        // Arrange
        var command = new AcceptFollowerPendingCommand(
            2,
            1,
            false);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }

    [Fact]
    public async Task AcceptFollowerPending_Handler_ShouldBeReturnErrorWhenFollowerNotFound()
    {
        // Arrange
        var command = new AcceptFollowerPendingCommand(
            2,
            100,
            false);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.FollowersNotFound, result.Errors[0].Message);
    }
}