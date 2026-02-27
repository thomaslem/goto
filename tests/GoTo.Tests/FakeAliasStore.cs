using GoTo.Data;

namespace GoTo.Tests;

public class FakeAliasStore(Dictionary<string, string>? aliases = null) : IAliasStore
{
    private readonly Dictionary<string, string> _aliases = aliases ?? new Dictionary<string, string>();
    
    public string? Get(string alias)
    {
        _aliases.TryGetValue(alias, out var value);
        return value;
    }

    public void Add(string alias, string folderPath) =>
        _aliases.Add(alias, folderPath);

    public void Remove(string alias) =>
        _aliases.Remove(alias);
}