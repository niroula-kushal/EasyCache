using System.Text.Json;
using EasyCacher.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace EasyCache;

public class EasyCacheClient : IEasyCacheClient
{
    private readonly IMemoryCache _memoryCache;
    private readonly IEasyCacheKeyManager _easyCacheKeyManager;

    public EasyCacheClient(IMemoryCache memoryCache, IEasyCacheKeyManager easyCacheKeyManager)
    {
        _memoryCache = memoryCache;
        _easyCacheKeyManager = easyCacheKeyManager;
    }

    public IEasyCacheManager<T> UseCache<T>(Func<Task<T>> retriever, object cacheKey,
        MemoryCacheEntryOptions? cacheOptions = null)
    {
        ManageKeys(cacheKey);
        return new EasyCacheManager<T>(_memoryCache, retriever, cacheKey, cacheOptions);
    }

    public void InvalidateCache(object cacheKey)
    {
        _memoryCache.Remove(cacheKey);
    }

    private void ManageKeys(object cacheKey)
    {
        string stringifiedKey;
        if (cacheKey is not string)
        {
            stringifiedKey = JsonSerializer.Serialize(cacheKey);
        }
        else stringifiedKey = cacheKey.ToString();

        _easyCacheKeyManager.AddKey(stringifiedKey);
    }
}