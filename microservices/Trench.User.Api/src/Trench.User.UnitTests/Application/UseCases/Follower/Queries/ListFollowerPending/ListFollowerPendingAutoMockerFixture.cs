using Moq;
using Moq.AutoMock;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Application.UseCases.Follower.Queries.ListFollowerPending;
using Trench.User.Domain.Aggregates.Follower.Dtos;
using Trench.User.UnitTests.Config;

namespace Trench.User.UnitTests.Application.UseCases.Follower.Queries.ListFollowerPending;

[CollectionDefinition(nameof(ListFollowerPendingAutoMockerFixtureCollection))]
public class ListFollowerPendingAutoMockerFixtureCollection : IClassFixture<ListFollowerPendingAutoMockerFixture>;

public class ListFollowerPendingAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new();

    internal ListFollowerPendingQueryHandler GetInstance()
    {
        AutoMocker = new AutoMocker();
        return AutoMocker.CreateInstance<ListFollowerPendingQueryHandler>();
    }

    public void MockListFollowersPending()
    {
        AutoMocker.GetMock<IFollowerRepository>()
            .Setup(x => x.ListFollowersPending(It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync([GetFollowersPendingDto()]);
    }

    #region Private Methods

    private ListFollowersPendingDto GetFollowersPendingDto()
    {
        return new ListFollowersPendingDto(
            Faker.Random.Int(),
            Faker.Internet.Url(),
            Faker.Person.FullName);
    }

    #endregion
    
}