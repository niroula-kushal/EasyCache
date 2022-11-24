using EasyCacher.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EasyCache;

public static class DiConfig
{
    public static IServiceCollection UseEasyCache(this IServiceCollection services)
    {
        services.AddSingleton<IEasyCache, EasyCache>();
        return services;
    }
}