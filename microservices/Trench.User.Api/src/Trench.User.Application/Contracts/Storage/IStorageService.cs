namespace Pulse.Product.Application.Contracts.Storage;

public interface IStorageService
{
    Task<string> GeneratePresignUrlAsync(string key, string contentType, CancellationToken cancellationToken);

    Task<string> GetPresignUrlAsync(string key, CancellationToken cancellationToken);

    Task<bool> ReplaceObject(string key, Stream stream, CancellationToken cancellationToken);

    Task<bool> PutObject(string key, Stream stream, CancellationToken cancellationToken);

    Task<bool> DeleteObject(string key, CancellationToken cancellationToken);
}