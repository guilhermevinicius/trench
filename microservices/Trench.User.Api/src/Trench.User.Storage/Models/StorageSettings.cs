namespace Trench.User.Storage.Models;

public class StorageSettings
{
    public required string ServerUrl { get; init; }
    public required string AccessKey { get; init; }
    public required string SecretKey { get; init; }
    public required string BucketName { get; init; }
}