using Trench.User.Application.UseCases.User.Commands.RegisterUser;
using Trench.User.Domain.Resources;
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
    
    [Fact]
    public async Task RegisterUser_Handler_ShouldBeReturnErrorWhenValidatorFailed()
    {
        // Arrange
        var command = new RegisterUserCommand(
            "",
            Faker.Person.LastName,
            Faker.Person.Email,
            Faker.Person.UserName,
            DateTime.UtcNow,
            Faker.Internet.Password());

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Errors);
    }
    
    [Fact]
    public async Task RegisterUser_Handler_ShouldBeReturnErrorWhenUsernameAlreadyExists()
    {
        // Arrange
        var command = new RegisterUserCommand(
            Faker.Person.FirstName,
            Faker.Person.LastName,
            Faker.Person.Email,
            "trench",
            DateTime.UtcNow,
            Faker.Internet.Password());

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(DomainValidationResource.AlreadyUsernameExists, result.Errors[0].Message);;
    }

    [Fact]
    public async Task RegisterUser_Handler_ShouldBeReturnErrorWhenEmailAlreadyExists()
    {
        // Arrange
        var command = new RegisterUserCommand(
            Faker.Person.FirstName,
            Faker.Person.LastName,
            "trench@trench.com",
            Faker.Person.UserName,
            DateTime.UtcNow,
            Faker.Internet.Password());

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(DomainValidationResource.AlreadyEmailExists, result.Errors[0].Message);;
    }
}