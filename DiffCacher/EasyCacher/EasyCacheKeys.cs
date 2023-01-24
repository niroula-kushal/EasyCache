using EasyCacher.Abstractions;

namespace EasyCache;

public class EasyCacheKeyManager : IEasyCacheKeyManager
{
    private HashSet<string> Keys = new HashSet<string>();

    public HashSet<string> GetKeys() => new HashSet<string>(Keys);

    public void AddKey(string key)
    {
        if (Keys.Contains(key)) return;
        Keys.Add(key);
    }
}