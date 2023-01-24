using EasyCacher.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EasyCache;

public static class DiConfig
{
    public static IServiceCollection UseEasyCache(this IServiceCollection services)
    {
        services.AddSingleton<IEasyCacheKeyManager, EasyCacheKeyManager>();
        services.AddSingleton<IEasyCacheClient, EasyCacheClient>();
        return services;
    }
}