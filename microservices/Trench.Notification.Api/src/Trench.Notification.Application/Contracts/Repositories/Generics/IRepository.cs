namespace Trench.Notification.Application.Contracts.Repositories.Generics;

public interface IRepository<T>
{
    Task InsertAsync(T entity, CancellationToken cancellationToken);
}