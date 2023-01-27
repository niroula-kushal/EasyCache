# EasyCache
Easy Cache For C#. Supports Inmemory cache only for now

### Usage

1. Add EasyCache to you DI

```csharp
// startup.cs or program.cs
builder.Services.AddMemoryCache();
builder.Services.UseEasyCache();
```

2. Inject IEasyCacheClient to use it.

3. Define a method to retrieve data

4. Call UseCache

```csharp
// Creates a cache manager that returns List<WeatherForecast>, uses the method GetForecast to get data.
// Cache is maintained using keys. Here, the second parameter, an object, is the key.
// Key can be anything, string, object.

// To get the actual data, call cache.GetData(). Its an ansyn function that returns the data, either from cache or from your source.

var cache = _easyCacheClient.UseCache<List<WeatherForecast>>(GetForecast, new
{
    key = "weather_items",
    id = 45
});
var keys = _easyCacheKeyManager.GetKeys();
return Ok(new
{
    data = (await cache.GetData()).ToList(),
    keys = keys
});

```

5. The default cache duration is 1 minute, after that, the cache will be invalidated and the next time the cache is used, it will invoke your function to get the data.
6. This library uses MemoryCache under the hood, and the cache configuration is possible using https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.caching.memory.memorycacheentryoptions?view=dotnet-plat-ext-7.0 .

### Methods

1. IEasyCacheManager#InvalidateCache

You can call this method to invalidate your cache. Once you call this, the next time the cache is accessed, your method will be called.

```csharp
var cache = _easyCacheClient.UseCache<List<WeatherForecast>>(GetForecast, new
{
    key = "weather_items",
    id = 45
});
// perform some task

cache.InvalidateCache()

```

2. IEasyCacheClient#InvalidateCache

This method performs identical to above method, but requires that cache key be present

```csharp
_easyCacheClient.InvalidateCache(new
{
    key = "weather_items",
    id = 45
});

```

3. IEasyCacheKeys#GetKeys

This method can be called to retrieve the list of cache keys registered in your application. Will only contain the keys that have been used.
