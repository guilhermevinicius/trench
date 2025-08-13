using Trench.User.Application.UseCases.Follower.Queries.ListFollowerPending;
using Trench.User.UnitTests.Config;

namespace Trench.User.UnitTests.Application.UseCases.Follower.Queries.ListFollowerPending;

[Collection(nameof(ListFollowerPendingAutoMockerFixtureCollection))]
public class ListFollowerPendingTest(ListFollowerPendingAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task ListFollowerPending_Handler_ShouldBeReturnSuccess()
    {
        // Arrange
        var query = new ListFollowerPendingQuery(Faker.Random.Int());

        // Action
        var handler = fixture.GetInstance();
        fixture.MockListFollowersPending();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEmpty(result.Value);
    }
    
    [Fact]
    public async Task ListFollowerPending_Handler_ShouldBeReturnEmptyWhenNotFollowerPending()
    {
        // Arrange
        var query = new ListFollowerPendingQuery(Faker.Random.Int());

        // Action
        var handler = fixture.GetInstance();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }
}