namespace EasyCacher.Abstractions;

public interface IEasyCacheManager<T>
{
    Task<T> GetData();
    void InvalidateCache();
}