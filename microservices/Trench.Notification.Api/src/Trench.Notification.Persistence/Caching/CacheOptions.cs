using Microsoft.Extensions.Caching.Distributed;

namespace Trench.Notification.Persistence.Caching;

internal static class CacheOptions
{
    private static DistributedCacheEntryOptions DefaultExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
    };

    internal static DistributedCacheEntryOptions Create(TimeSpan? expiration)
    {
        return new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration ?? null };
    }
}