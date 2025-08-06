using Moq;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Application.UseCases.User.Commands.RegisterUser;
using Trench.User.Domain.Resources;
using Trench.User.Domain.SeedWorks;
using Trench.User.UnitTests.Config;
using Entity = Trench.User.Domain.Aggregates.Users.Entities;

namespace Trench.User.UnitTests.Application.UseCases.User.RegisterUser;

[Collection(nameof(RegisterUserAutoMockerCollection))]
public class RegisterUserTest(RegisterUserAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task RegisterUser_Handler_ShouldBeRegisterSuccess()
    {
        // Arrange
        var command = new RegisterUserCommand(
            Faker.Person.FirstName,
            Faker.Person.LastName,
            Faker.Person.Email,
            Faker.Person.UserName,
            Faker.Person.DateOfBirth,
            Faker.Random.String2(10));

        // Action
        var handler = fixture.GetInstance();
        fixture.MockAlreadyUsernameExists();
        fixture.MockAlreadyEmailExists();
        fixture.MockIntegrationIdentity();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.AlreadyUsernameExists(It.IsAny<string>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.AlreadyEmailExists(It.IsAny<string>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.InsertAsync(It.IsAny<Entity.User>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUnitOfWork>(x => 
            x.CommitAsync(CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task RegisterUser_Handler_ShouldBeReturnErrorWhenAlreadyUsernameExists()
    {
        // Arrange
        var command = new RegisterUserCommand(
            Faker.Person.FirstName,
            Faker.Person.LastName,
            Faker.Person.Email,
            Faker.Person.UserName,
            Faker.Person.DateOfBirth,
            Faker.Random.String2(10));

        // Action
        var handler = fixture.GetInstance();
        fixture.MockAlreadyUsernameExists(true);
        fixture.MockAlreadyEmailExists();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal(DomainValidationResource.AlreadyUsernameExists, result.Errors[0].Message);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.AlreadyUsernameExists(It.IsAny<string>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.AlreadyEmailExists(It.IsAny<string>(), CancellationToken.None), Times.Never);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.InsertAsync(It.IsAny<Entity.User>(), CancellationToken.None), Times.Never);
        fixture.AutoMocker.Verify<IUnitOfWork>(x => 
            x.CommitAsync(CancellationToken.None), Times.Never);
    }
    
    [Fact]
    public async Task RegisterUser_Handler_ShouldBeReturnErrorWhenAlreadyEmailExists()
    {
        // Arrange
        var command = new RegisterUserCommand(
            Faker.Person.FirstName,
            Faker.Person.LastName,
            Faker.Person.Email,
            Faker.Person.UserName,
            Faker.Person.DateOfBirth,
            Faker.Random.String2(10));

        // Action
        var handler = fixture.GetInstance();
        fixture.MockAlreadyUsernameExists();
        fixture.MockAlreadyEmailExists(true);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal(DomainValidationResource.AlreadyEmailExists, result.Errors[0].Message);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.AlreadyUsernameExists(It.IsAny<string>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.AlreadyEmailExists(It.IsAny<string>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.InsertAsync(It.IsAny<Entity.User>(), CancellationToken.None), Times.Never);
        fixture.AutoMocker.Verify<IUnitOfWork>(x => 
            x.CommitAsync(CancellationToken.None), Times.Never);
    }
    
    [Fact]
    public async Task RegisterUser_Handler_ShouldBeReturnErrorWhenIdentityFailed()
    {
        // Arrange
        var command = new RegisterUserCommand(
            Faker.Person.FirstName,
            Faker.Person.LastName,
            Faker.Person.Email,
            Faker.Person.UserName,
            Faker.Person.DateOfBirth,
            Faker.Random.String2(10));

        // Action
        var handler = fixture.GetInstance();
        fixture.MockAlreadyUsernameExists();
        fixture.MockAlreadyEmailExists();
        fixture.MockIntegrationIdentity(false);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal(DomainValidationResource.ErrorCreatingAccount, result.Errors[0].Message);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.AlreadyUsernameExists(It.IsAny<string>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.AlreadyEmailExists(It.IsAny<string>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.InsertAsync(It.IsAny<Entity.User>(), CancellationToken.None), Times.Never);
        fixture.AutoMocker.Verify<IUnitOfWork>(x => 
            x.CommitAsync(CancellationToken.None), Times.Never);
    }
}