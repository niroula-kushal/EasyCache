namespace EasyCache;

public interface IEasyCacheManager<T>
{
    Task<T> GetData();
    void InvalidateCache();
}