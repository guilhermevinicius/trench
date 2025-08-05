using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using Pulse.Product.Application.Contracts.Storage;
using Pulse.Product.Storage.Models;

namespace Pulse.Product.Storage.Services;

internal sealed class StorageService(
    IAmazonS3 amazonS3,
    IOptions<StorageSettings> storageSettings)
    : IStorageService
{
    public async Task<string> GeneratePresignUrlAsync(string key, string contentType,
        CancellationToken cancellationToken)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = storageSettings.Value.BucketName,
            Key = key,
            Verb = HttpVerb.PUT,
            Expires = DateTime.UtcNow.AddMinutes(15),
            ContentType = contentType
        };

        return await amazonS3.GetPreSignedURLAsync(request);
    }

    public async Task<string> GetPresignUrlAsync(string key, CancellationToken cancellationToken)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = storageSettings.Value.BucketName,
            Key = key,
            Verb = HttpVerb.GET,
            Expires = DateTime.UtcNow.AddMinutes(15)
        };

        return await amazonS3.GetPreSignedURLAsync(request);
    }

    public Task<bool> ReplaceObject(string key, Stream stream, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> PutObject(string key, Stream stream, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteObject(string key, CancellationToken cancellationToken)
    {
        var objectResponse = await amazonS3.DeleteObjectAsync(
            storageSettings.Value.BucketName,
            key,
            cancellationToken);

        return objectResponse.HttpStatusCode == System.Net.HttpStatusCode.OK;   
    }
}