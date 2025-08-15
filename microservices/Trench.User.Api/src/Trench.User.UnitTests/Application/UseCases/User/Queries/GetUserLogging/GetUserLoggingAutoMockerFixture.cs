using Moq;
using Moq.AutoMock;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Application.UseCases.User.Queries.GetUserLogging;
using Trench.User.Domain.Aggregates.Users.Dtos;
using Trench.User.UnitTests.Config;

namespace Trench.User.UnitTests.Application.UseCases.User.Queries.GetUserLogging;

[CollectionDefinition(nameof(GetUserLoggingAutoMockerCollection))]
public class GetUserLoggingAutoMockerCollection : IClassFixture<GetUserLoggingAutoMockerFixture>;

public class GetUserLoggingAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new();

    internal GetUserLoggingQueryHandler GetInstance()
    {
        AutoMocker = new AutoMocker();
        return AutoMocker.CreateInstance<GetUserLoggingQueryHandler>();
    }

    public void MockUserLogging()
    {
        AutoMocker.GetMock<IUserRepository>()
            .Setup(x => x.GetUserLogging(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(GetUserLoggingScene);
    }

    #region Private Methods

    private GetUserLoggingDto GetUserLoggingScene()
    {
        return new GetUserLoggingDto(
            Faker.Person.FirstName,
            Faker.Person.LastName,
            Faker.Person.UserName,
            null);
    }

    #endregion

}