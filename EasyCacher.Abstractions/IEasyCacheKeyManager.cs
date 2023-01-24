namespace EasyCacher.Abstractions;

public interface IEasyCacheKeyManager
{
    HashSet<string> GetKeys();
    void AddKey(string key);
}