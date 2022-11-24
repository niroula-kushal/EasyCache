using EasyCacher.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace EasyCache;

public class EasyCache : IEasyCache
{
    private readonly IMemoryCache _memoryCache;

    public EasyCache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public IEasyCacheManager<T> UseCache<T>(Func<Task<T>> retriever, object cacheKey, MemoryCacheEntryOptions? cacheOptions = null)
    {
        return new EasyCacheManager<T>(_memoryCache, retriever, cacheKey, cacheOptions);
    }
}