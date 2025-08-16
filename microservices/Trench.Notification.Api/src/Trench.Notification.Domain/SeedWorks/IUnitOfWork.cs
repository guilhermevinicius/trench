namespace Trench.Notification.Domain.SeedWorks;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(CancellationToken cancellationToken);
}