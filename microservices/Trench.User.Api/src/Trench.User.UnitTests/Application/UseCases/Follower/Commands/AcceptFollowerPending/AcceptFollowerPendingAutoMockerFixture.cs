using Moq;
using Moq.AutoMock;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Application.UseCases.Follower.Commands.AcceptFollowerPending;
using Trench.User.Domain.Aggregates.Follower.Entities;
using Trench.User.Domain.SeedWorks;
using Trench.User.UnitTests.Config;

namespace Trench.User.UnitTests.Application.UseCases.Follower.Commands.AcceptFollowerPending;

[CollectionDefinition(nameof(AcceptFollowerPendingAutoMockerCollection))]
public class AcceptFollowerPendingAutoMockerCollection : IClassFixture<AcceptFollowerPendingAutoMockerFixture>;

public class AcceptFollowerPendingAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new();

    internal AcceptFollowerPendingCommandHandler GetInstance(bool isSuccess = true)
    {
        AutoMocker = new AutoMocker();
        MockCommit(isSuccess);
        return AutoMocker.CreateInstance<AcceptFollowerPendingCommandHandler>();
    }

    public void MockGetFollowersPending()
    {
        AutoMocker.GetMock<IFollowerRepository>()
            .Setup(x => x.GetFollowerPending(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(GetFollowersPendingScene);
    }

    #region Private Methods

    private void MockCommit(bool isSuccess = true)
    {
        AutoMocker.GetMock<IUnitOfWork>()
            .Setup(x => x.CommitAsync(CancellationToken.None))
            .ReturnsAsync(isSuccess);
    }

    private Followers GetFollowersPendingScene()
    {
        return Followers.Create(
            Faker.Random.Int(),
            Faker.Random.Int(),
            Faker.Random.Bool());
    }

    #endregion
}