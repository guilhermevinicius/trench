using Trench.User.Application.UseCases.User.Queries.GetUserByUsername;
using Trench.User.Domain.Resources;
using Trench.User.IntegrationTests.Config;

namespace Trench.User.IntegrationTests.Application.User.Queries.GetUserByUsername;

[Collection(nameof(IntegrationTestWebAppFactoryCollection))]
public class GetUserByUsernameTest(IntegrationTestWebAppFactory fixture) : BaseTest
{
    [Fact]
    public async Task GetUserByUsername_Handler_ShouldBeReturnUserLogging()
    {
        // Arrange
        var query = new GetUserByUsernameQuery("identityId", "trench");

        // Action
        var result = await fixture.Sender.Send(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public async Task GetUserByUsername_Handler_ShouldBeReturnErrorWhenValidatorFailed()
    {
        // Arrange
        var query = new GetUserByUsernameQuery("", "");

        // Action
        var result = await fixture.Sender.Send(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task GetUserByUsername_Handler_ShouldBeReturnErrorWhenUserNotFound()
    {
        // Arrange
        var query = new GetUserByUsernameQuery(
            Faker.Random.Guid().ToString(),
            "username-not-found");

        // Action
        var result = await fixture.Sender.Send(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.UserNotFound, result.Errors[0].Message);
    }
}