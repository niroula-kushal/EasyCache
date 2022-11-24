using EasyCache;
using Microsoft.Extensions.Caching.Memory;

namespace EasyCacher.Abstractions;

public interface IEasyCache
{
    IEasyCacheManager<T> UseCache<T>(Func<Task<T>> retriever, object cacheKey, MemoryCacheEntryOptions? cacheOptions = null);
}