using Moq;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Application.UseCases.User.Commands.Follower;
using Trench.User.Domain.Resources;
using Trench.User.Domain.SeedWorks;
using Trench.User.UnitTests.Config;

namespace Trench.User.UnitTests.Application.UseCases.User.Commands.Follower;

[Collection(nameof(FollowersAutoMockerCollection))]
public class FollowerTest(FollowerAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task Follower_Handler_ShouldBeSuccess()
    {
        // Arrange
        var command = new FollowerCommand(
            Faker.Random.Guid().ToString(),
            Faker.Random.Int(1, 100));

        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetByIdentityId();
        fixture.MockGetById();
        fixture.MockAlreadyFollowerExists();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.GetByIdentityId(It.IsAny<string>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.GetById(It.IsAny<int>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.AlreadyFollowerExists(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUnitOfWork>(x => 
            x.CommitAsync(CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task Follower_Handler_ShouldBeErrorWhenGetByIdentityIdNotFound()
    {
        // Arrange
        var command = new FollowerCommand(
            Faker.Random.Guid().ToString(),
            Faker.Random.Int(1, 100));

        // Action
        var handler = fixture.GetInstance();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.UserNotFound, result.Errors[0].Message);;
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.GetByIdentityId(It.IsAny<string>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.GetById(It.IsAny<int>(), CancellationToken.None), Times.Never);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.AlreadyFollowerExists(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None), Times.Never);
        fixture.AutoMocker.Verify<IUnitOfWork>(x => 
            x.CommitAsync(CancellationToken.None), Times.Never);
    }
    
    [Fact]
    public async Task Follower_Handler_ShouldBeErrorWhenFollowYourself()
    {
        // Arrange
        var command = new FollowerCommand(
            Faker.Random.Guid().ToString(),
            0);

        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetByIdentityId();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.FollowYourself, result.Errors[0].Message);;
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.GetByIdentityId(It.IsAny<string>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.GetById(It.IsAny<int>(), CancellationToken.None), Times.Never);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.AlreadyFollowerExists(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None), Times.Never);
        fixture.AutoMocker.Verify<IUnitOfWork>(x => 
            x.CommitAsync(CancellationToken.None), Times.Never);
    }
    
    [Fact]
    public async Task Follower_Handler_ShouldBeErrorWhenGetByFollowerNotFound()
    {
        // Arrange
        var command = new FollowerCommand(
            Faker.Random.Guid().ToString(),
            Faker.Random.Int(1, 100));

        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetByIdentityId();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.UserNotFound, result.Errors[0].Message);;
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.GetByIdentityId(It.IsAny<string>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.GetById(It.IsAny<int>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.AlreadyFollowerExists(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None), Times.Never);
        fixture.AutoMocker.Verify<IUnitOfWork>(x => 
            x.CommitAsync(CancellationToken.None), Times.Never);
    }
    
    [Fact]
    public async Task Follower_Handler_ShouldBeErrorWhenAlreadyFollowerExists()
    {
        // Arrange
        var command = new FollowerCommand(
            Faker.Random.Guid().ToString(),
            Faker.Random.Int(1, 100));

        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetByIdentityId();
        fixture.MockGetById();
        fixture.MockAlreadyFollowerExists(true);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.AlreadyFollowerExists, result.Errors[0].Message);;
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.GetByIdentityId(It.IsAny<string>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.GetById(It.IsAny<int>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.AlreadyFollowerExists(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUnitOfWork>(x => 
            x.CommitAsync(CancellationToken.None), Times.Never);
    }
}