namespace Pulse.Product.Storage.Models;

public class StorageSettings
{
    public string ServerUrl { get; private set; }
    public string AccessKey { get; init; }
    public string SecretKey { get; init; }
    public string BucketName { get; init; }
    
}