using Trench.User.Application.UseCases.User.Queries.GetUserByUsername;
using Trench.User.Domain.Resources;
using Trench.User.UnitTests.Config;

namespace Trench.User.UnitTests.Application.UseCases.User.Queries.GetUserByUsername;

[Collection(nameof(GetUserByUsernameAutoMockerFixtureCollection))]
public class GetUserByUsernameTest(GetUserByUsernameAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task GetUserByUsername_Handler_ShouldBeReturnSuccess()
    {
        // Arrange
        var query = new GetUserByUsernameQuery(
            Faker.Random.Guid().ToString(),
            Faker.Random.Guid().ToString());

        // Action
        var handler = fixture.GetInstance();
        fixture.MockUserLogging();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GetUserByUsername_Handler_ShouldBeReturnErrorWhenUserNotFound()
    {
        // Arrange
        var query = new GetUserByUsernameQuery(
            Faker.Random.Guid().ToString(),
            Faker.Random.Guid().ToString());

        // Action
        var handler = fixture.GetInstance();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.UserNotFound, result.Errors[0].Message);
    }
}