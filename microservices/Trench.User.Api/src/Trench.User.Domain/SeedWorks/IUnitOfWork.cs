namespace Trench.User.Domain.SeedWorks;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(CancellationToken cancellationToken);
}