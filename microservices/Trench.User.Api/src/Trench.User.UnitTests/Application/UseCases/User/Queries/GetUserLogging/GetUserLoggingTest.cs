using Trench.User.Application.UseCases.User.Queries.GetUserLogging;
using Trench.User.Domain.Resources;
using Trench.User.UnitTests.Config;

namespace Trench.User.UnitTests.Application.UseCases.User.Queries.GetUserLogging;

[Collection(nameof(GetUserLoggingAutoMockerCollection))]
public class GetUserLoggingTest(GetUserLoggingAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task GetUserLogging_Handler_ShouldBeReturnSuccess()
    {
        // Arrange
        var query = new GetUserLoggingQuery(
            Faker.Random.Int());

        // Action
        var handler = fixture.GetInstance();
        fixture.MockUserLogging();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GetUserLogging_Handler_ShouldBeReturnErrorWhenUserNotFound()
    {
        // Arrange
        var query = new GetUserLoggingQuery(
            Faker.Random.Int());

        // Action
        var handler = fixture.GetInstance();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.UserNotFound, result.Errors[0].Message);
    }
}