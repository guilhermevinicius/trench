using Trench.User.Application.UseCases.User.Queries.GetUserLogging;
using Trench.User.Domain.Resources;
using Trench.User.IntegrationTests.Config;

namespace Trench.User.IntegrationTests.Application.User.Queries.GetUserLogging;

[Collection(nameof(IntegrationTestWebAppFactoryCollection))]
public class GetUserLoggingTest(IntegrationTestWebAppFactory fixture) : BaseTest
{
    [Fact]
    public async Task GetUserLogging_Handler_ShouldBeReturnUserLogging()
    {
        // Arrange
        var query = new GetUserLoggingQuery(1);

        // Action
        var result = await fixture.Sender.Send(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public async Task GetUserLogging_Handler_ShouldBeReturnErrorWhenValidatorFailed()
    {
        // Arrange
        var query = new GetUserLoggingQuery(0);

        // Action
        var result = await fixture.Sender.Send(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task GetUserLogging_Handler_ShouldBeReturnErrorWhenUserNotFound()
    {
        // Arrange
        var query = new GetUserLoggingQuery(Faker.Random.Int());

        // Action
        var result = await fixture.Sender.Send(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.UserNotFound, result.Errors[0].Message);
    }
}