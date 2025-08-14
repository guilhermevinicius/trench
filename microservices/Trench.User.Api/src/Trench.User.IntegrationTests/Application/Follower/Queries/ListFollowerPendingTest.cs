using Trench.User.Application.UseCases.Follower.Queries.ListFollowerPending;
using Trench.User.IntegrationTests.Config;

namespace Trench.User.IntegrationTests.Application.Follower.Queries;

[Collection(nameof(IntegrationTestWebAppFactoryCollection))]
public class ListFollowerPendingTest(IntegrationTestWebAppFactory fixture)
{
    [Fact]
    public async Task ListFollowerPending_Handler_ShouldBeReturnFollowerPending()
    {
        // Arrange
        var query = new ListFollowerPendingQuery(1);

        // Action
        var result = await fixture.Sender.Send(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(result.Value);
    }
    
    [Fact]
    public async Task ListFollowerPending_Handler_ShouldBeReturnEmptyWhenNotFollowerPending()
    {
        // Arrange
        var query = new ListFollowerPendingQuery(3);

        // Action
        var result = await fixture.Sender.Send(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }
}