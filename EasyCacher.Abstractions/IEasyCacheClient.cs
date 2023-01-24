using Microsoft.Extensions.Caching.Memory;

namespace EasyCacher.Abstractions;

public interface IEasyCacheClient
{
    IEasyCacheManager<T> UseCache<T>(Func<Task<T>> retriever, object cacheKey,
        MemoryCacheEntryOptions? cacheOptions = null);

    void InvalidateCache(object cacheKey);
}