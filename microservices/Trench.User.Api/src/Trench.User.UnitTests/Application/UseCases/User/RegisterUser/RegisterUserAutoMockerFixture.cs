using Moq;
using Moq.AutoMock;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Application.UseCases.User.Commands.RegisterUser;
using Trench.User.Domain.Integrations;
using Trench.User.Domain.SeedWorks;
using Trench.User.UnitTests.Config;
using Entity = Trench.User.Domain.Aggregates.Users.Entities;

namespace Trench.User.UnitTests.Application.UseCases.User.RegisterUser;

[CollectionDefinition(nameof(RegisterUserAutoMockerCollection))]
public class RegisterUserAutoMockerCollection : IClassFixture<RegisterUserAutoMockerFixture>;

public class RegisterUserAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new();

    internal RegisterUserCommandHandler GetInstance(bool isSuccess = true)
    {
        AutoMocker = new AutoMocker();
        MockCommit(isSuccess);
        return AutoMocker.CreateInstance<RegisterUserCommandHandler>();
    }

    public void MockAlreadyUsernameExists(bool isExists = false)
    {
        AutoMocker.GetMock<IUserRepository>()
            .Setup(x => x.AlreadyUsernameExists(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(isExists);
    }

    public void MockAlreadyEmailExists(bool isExists = false)
    {
        AutoMocker.GetMock<IUserRepository>()
            .Setup(x => x.AlreadyEmailExists(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(isExists);
    }

    public void MockIntegrationIdentity(bool isSuccess = true)
    {
        AutoMocker.GetMock<IIntegrationIdentity>()
            .Setup(x => x.RegisterAsync(It.IsAny<Entity.User>(), It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(isSuccess ? Faker.Random.Guid().ToString() : null);
    }

    #region Priva Methods

    private void MockCommit(bool isSuccess = true)
    {
        AutoMocker.GetMock<IUnitOfWork>()
            .Setup(x => x.CommitAsync(CancellationToken.None))
            .ReturnsAsync(isSuccess);
    }

    #endregion
    
}