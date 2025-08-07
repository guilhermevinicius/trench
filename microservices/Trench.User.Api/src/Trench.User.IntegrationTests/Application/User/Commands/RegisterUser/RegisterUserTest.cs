using Trench.User.Application.UseCases.User.Commands.RegisterUser;
using Trench.User.IntegrationTests.Config;

namespace Trench.User.IntegrationTests.Application.User.Commands.RegisterUser;

[Collection(nameof(IntegrationTestWebAppFactoryCollection))]
public class RegisterUserTest(IntegrationTestWebAppFactory fixture) : BaseTest
{
    [Fact]
    public async Task RegisterUser_Handler_ShouldBeRegisterUserCorrectly()
    {
        // Arrange
        var command = new RegisterUserCommand(
            Faker.Person.FirstName,
            Faker.Person.LastName,
            Faker.Person.Email,
            Faker.Person.UserName,
            DateTime.UtcNow,
            Faker.Internet.Password());

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }
}