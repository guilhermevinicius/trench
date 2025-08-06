using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trench.User.Application.Contracts.Storage;
using Trench.User.Storage.Models;
using Trench.User.Storage.Services;

namespace Trench.User.Storage.Configurations;

public static class StorageDependencyInjection
{
    public static IServiceCollection ConfigureStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StorageSettings>(sp => 
            configuration.GetSection(nameof(StorageSettings)).Bind(sp));

        services.AddSingleton<IAmazonS3>(opt =>
        {
            var credentials = new BasicAWSCredentials(
                configuration["StorageSettings:AccessKey"],
                configuration["StorageSettings:SecretKey"]);

            var config = new AmazonS3Config
            {
                ServiceURL = configuration["StorageSettings:ServerUrl"],
                ForcePathStyle = true
            };

            return new AmazonS3Client(credentials, config);
        });

        services.AddScoped<IStorageService, StorageService>();

        return services;
    }
}