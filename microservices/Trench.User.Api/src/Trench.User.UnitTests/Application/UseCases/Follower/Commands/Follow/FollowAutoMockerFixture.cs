using Moq;
using Moq.AutoMock;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Application.UseCases.Follower.Commands.Follow;
using Trench.User.Domain.SeedWorks;
using Trench.User.UnitTests.Config;
using Entity = Trench.User.Domain.Aggregates.Users.Entities;

namespace Trench.User.UnitTests.Application.UseCases.Follower.Commands.Follow;

[CollectionDefinition(nameof(FollowAutoMockerCollection))]
public class FollowAutoMockerCollection : IClassFixture<FollowAutoMockerFixture>;

public class FollowAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new();

    internal FollowCommandHandler GetInstance(bool isSuccess = true)
    {
        AutoMocker = new AutoMocker();
        MockCommit(isSuccess);
        return AutoMocker.CreateInstance<FollowCommandHandler>();
    }

    public void MockAlreadyFollowerExists(bool isExists = false)
    {
        AutoMocker.GetMock<IFollowerRepository>()
            .Setup(x => x.AlreadyFollowerExists(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(isExists);
    }

    public void MockUserById()
    {
        AutoMocker.GetMock<IUserRepository>()
            .Setup(x => x.GetById(It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(GetUserScene);
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