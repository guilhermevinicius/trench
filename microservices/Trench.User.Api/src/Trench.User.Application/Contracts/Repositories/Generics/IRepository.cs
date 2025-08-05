namespace Pulse.Product.Application.Contracts.Repositories.Generics;

public interface IRepository<T>
{
    Task InsertAsync(T entity, CancellationToken cancellationToken);
}