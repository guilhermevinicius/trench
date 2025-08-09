using Moq;
using Moq.AutoMock;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Application.UseCases.User.Commands.Follower;
using Trench.User.Domain.SeedWorks;
using Trench.User.UnitTests.Config;
using Entity = Trench.User.Domain.Aggregates.Users.Entities;

namespace Trench.User.UnitTests.Application.UseCases.User.Commands.Follower;

[CollectionDefinition(nameof(FollowersAutoMockerCollection))]
public class FollowersAutoMockerCollection : IClassFixture<FollowerAutoMockerFixture>;

public class FollowerAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new();

    internal FollowerCommandHandler GetInstance(bool isSuccess = true)
    {
        AutoMocker = new AutoMocker();
        MockCommit(isSuccess);
        return AutoMocker.CreateInstance<FollowerCommandHandler>();
    }

    public void MockGetByIdentityId()
    {
        AutoMocker.GetMock<IUserRepository>()
            .Setup(x => x.GetByIdentityId(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(GetUserScene);
    }

    public void MockGetById()
    {
        AutoMocker.GetMock<IUserRepository>()
            .Setup(x => x.GetById(It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(GetUserScene);
    }

    public void MockAlreadyFollowerExists(bool isExists = false)
    {
        AutoMocker.GetMock<IUserRepository>()
            .Setup(x => x.AlreadyFollowerExists(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(isExists);
    }

    #region Private Methods

    private void MockCommit(bool isSuccess = true)
    {
        AutoMocker.GetMock<IUnitOfWork>()
            .Setup(x => x.CommitAsync(CancellationToken.None))
            .ReturnsAsync(isSuccess);
    }

    private Entity.User GetUserScene()
    {
        return Entity.User.Create(
            Faker.Name.FirstName(),
            Faker.Name.LastName(),
            Faker.Internet.Email(),
            Faker.Internet.UserName(),
            Faker.Date.Past(18));
    }

    #endregion
}