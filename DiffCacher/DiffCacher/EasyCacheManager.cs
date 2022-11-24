using Microsoft.Extensions.Caching.Memory;

namespace EasyCache;

public class EasyCacheManager<T> : IEasyCacheManager<T>
{
    private readonly IMemoryCache _memoryCache;
    private readonly Func<Task<T>> _retriever;
    private readonly object _cacheKey;
    private readonly MemoryCacheEntryOptions? _cacheOptions;

    public EasyCacheManager(IMemoryCache memoryCache, Func<Task<T>> retriever, object cacheKey, MemoryCacheEntryOptions? cacheOptions = null)
    {
        _memoryCache = memoryCache;
        _retriever = retriever;
        _cacheKey = cacheKey;
        _cacheOptions = cacheOptions ?? new MemoryCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        };
    }

    public async Task<T> GetData()
    {
        if (_memoryCache.TryGetValue<T>(_cacheKey, out var cachedValue))
        {
            return cachedValue!;
        }

        var value = await _retriever.Invoke();
        _memoryCache.Set(_cacheKey, value, _cacheOptions);
        return value;
    }

    public void InvalidateCache()
    {
        _memoryCache.Remove(_cacheKey);
    }
}