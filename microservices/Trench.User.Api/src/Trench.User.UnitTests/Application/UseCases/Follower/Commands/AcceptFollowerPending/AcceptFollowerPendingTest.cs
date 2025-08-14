using Trench.User.Application.UseCases.Follower.Commands.AcceptFollowerPending;
using Trench.User.Domain.Resources;
using Trench.User.UnitTests.Config;

namespace Trench.User.UnitTests.Application.UseCases.Follower.Commands.AcceptFollowerPending;

[Collection(nameof(AcceptFollowerPendingAutoMockerCollection))]
public class AcceptFollowerPendingTest(AcceptFollowerPendingAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task AcceptFollowerPending_Handler_ShouldBeReturnAccept()
    {
        // Arrange
        var command = new AcceptFollowerPendingCommand(
            Faker.Random.Int(),
            Faker.Random.Int(),
            true);

        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetFollowersPending();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }

    [Fact]
    public async Task AcceptFollowerPending_Handler_ShouldBeReturnRejected()
    {
        // Arrange
        var command = new AcceptFollowerPendingCommand(
            Faker.Random.Int(),
            Faker.Random.Int(),
            false);

        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetFollowersPending();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }

    [Fact]
    public async Task AcceptFollowerPending_Handler_ShouldBeReturnErrorWhenFollowerNotFound()
    {
        // Arrange
        var command = new AcceptFollowerPendingCommand(
            Faker.Random.Int(),
            Faker.Random.Int(),
            Faker.Random.Bool());

        // Action
        var handler = fixture.GetInstance();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.FollowersNotFound, result.Errors[0].Message);
    }
}