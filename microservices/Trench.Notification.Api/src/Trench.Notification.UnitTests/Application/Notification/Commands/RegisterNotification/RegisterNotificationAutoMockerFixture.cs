using Moq;
using Moq.AutoMock;
using Trench.Notification.Application.UseCases.Notifications.Commands.RegisterNotification;
using Trench.Notification.Domain.SeedWorks;
using Trench.Notification.UnitTests.Config;

namespace Trench.Notification.UnitTests.Application.Notification.Commands.RegisterNotification;

[CollectionDefinition(nameof(RegisterNotificationAutoMockerCollection))]
public class RegisterNotificationAutoMockerCollection : IClassFixture<RegisterNotificationAutoMockerFixture>;

public class RegisterNotificationAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new();

    internal RegisterNotificationCommandHandler GetInstance(bool isSuccess = true)
    {
        AutoMocker = new AutoMocker();
        MockCommit(isSuccess);
        return AutoMocker.CreateInstance<RegisterNotificationCommandHandler>();
    }

    #region Private Methods

    private void MockCommit(bool isSuccess = true)
    {
        AutoMocker.GetMock<IUnitOfWork>()
            .Setup(x => x.CommitAsync(CancellationToken.None))
            .ReturnsAsync(isSuccess);
    }

    #endregion
    
}