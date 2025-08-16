using Moq;
using Moq.AutoMock;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Application.UseCases.User.Queries.GetUserByUsername;
using Trench.User.Domain.Aggregates.Users.Dtos;
using Trench.User.UnitTests.Config;

namespace Trench.User.UnitTests.Application.UseCases.User.Queries.GetUserByUsername;

[CollectionDefinition(nameof(GetUserByUsernameAutoMockerFixtureCollection))]
public class GetUserByUsernameAutoMockerFixtureCollection : IClassFixture<GetUserByUsernameAutoMockerFixture>;

public class GetUserByUsernameAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new();

    internal GetUserByUsernameQueryHandler GetInstance()
    {
        AutoMocker = new AutoMocker();
        return AutoMocker.CreateInstance<GetUserByUsernameQueryHandler>();
    }

    public void MockUserLogging()
    {
        AutoMocker.GetMock<IUserRepository>()
            .Setup(x => x.GetByUsername(It.IsAny<string>(), It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(GetUserByUsernameScene);
    }

    #region Private Methods

    private GetUserByUsernameDto GetUserByUsernameScene()
    {
        return new GetUserByUsernameDto(
            Faker.Random.Int(),
            Faker.Person.FirstName,
            Faker.Person.LastName,
            Faker.Person.UserName,
            Faker.Lorem.Letter(10),
            Faker.Random.Bool(),
            Faker.Random.Bool(),
            Faker.Random.Bool());
    }

    #endregion

}