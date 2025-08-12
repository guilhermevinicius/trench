using Moq;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Application.UseCases.Follower.Commands.Follow;
using Trench.User.Domain.Aggregates.Follower.Entities;
using Trench.User.Domain.Resources;
using Trench.User.Domain.SeedWorks;
using Trench.User.UnitTests.Config;

namespace Trench.User.UnitTests.Application.UseCases.Follower.Commands.Follow;

[Collection(nameof(FollowAutoMockerCollection))]
public class FollowTest(FollowAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task Follow_Handler_ShouldBeReturnSuccess()
    {
        // Arrange
        var command = new FollowCommand(
            Faker.Random.Int(),
            Faker.Random.Int());

        // Action
        var handler = fixture.GetInstance();
        fixture.MockAlreadyFollowerExists();
        fixture.MockUserById();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
        fixture.AutoMocker.Verify<IFollowerRepository>(x => 
            x.AlreadyFollowerExists(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.GetById(It.IsAny<int>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IFollowerRepository>(x => 
            x.InsertAsync(It.IsAny<Followers>(), CancellationToken.None));
        fixture.AutoMocker.Verify<IUnitOfWork>(x => 
            x.CommitAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Follow_Handler_ShouldBeReturnErrorWhenAlreadyExists()
    {
        // Arrange
        var command = new FollowCommand(
            Faker.Random.Int(),
            Faker.Random.Int());

        // Action
        var handler = fixture.GetInstance();
        fixture.MockAlreadyFollowerExists(true);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.AlreadyFollowerExists, result.Errors[0].Message);
        fixture.AutoMocker.Verify<IFollowerRepository>(x => 
            x.AlreadyFollowerExists(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x => 
            x.GetById(It.IsAny<int>(), CancellationToken.None), Times.Never);
        fixture.AutoMocker.Verify<IFollowerRepository>(x => 
            x.InsertAsync(It.IsAny<Followers>(), CancellationToken.None), Times.Never);
        fixture.AutoMocker.Verify<IUnitOfWork>(x => 
            x.CommitAsync(CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Follow_Handler_ShouldBeReturnErrorWhenFollowingNotFound()
    {
        // Arrange
        var command = new FollowCommand(
            Faker.Random.Int(),
            Faker.Random.Int());

        // Action
        var handler = fixture.GetInstance();
        fixture.MockAlreadyFollowerExists();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainValidationResource.UserNotFound, result.Errors[0].Message);
        fixture.AutoMocker.Verify<IFollowerRepository>(x => 
            x.AlreadyFollowerExists(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IUserRepository>(x =>
            x.GetById(It.IsAny<int>(), CancellationToken.None), Times.Once);
        fixture.AutoMocker.Verify<IFollowerRepository>(x => 
            x.InsertAsync(It.IsAny<Followers>(), CancellationToken.None), Times.Never);
        fixture.AutoMocker.Verify<IUnitOfWork>(x => 
            x.CommitAsync(CancellationToken.None), Times.Never);
    }
}