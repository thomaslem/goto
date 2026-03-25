using System.Text.Json;

namespace GoTo.Data;

public interface IAliasStore
{
	string? Get(string alias);

	void Add(string alias, string folderPath);

	void Remove(string alias);
}

internal class AliasStore : IAliasStore
{
	private const string FolderName = ".goto";
	private const string FileName = "aliases.json";

	private static readonly string FolderPath =
		Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), FolderName);

	private static readonly string FilePath = Path.Combine(FolderPath, FileName);

	private static readonly JsonSerializerOptions JsonSerializerOptions = new()
	{
		WriteIndented = true
	};

	public string? Get(string alias)
	{
		var aliases = Load();

		return aliases.TryGetValue(alias, out var folderPath)
			? folderPath
			: aliases.FirstOrDefault(a => a.Key.Contains(alias, StringComparison.OrdinalIgnoreCase)).Value;
	}

	public void Add(string alias, string folderPath)
	{
		var aliases = Load();
		aliases[alias] = folderPath;
		Save(aliases);
	}

	public void Remove(string alias)
	{
		var aliases = Load();
		aliases.Remove(alias);
		Save(aliases);
	}

	private static Dictionary<string, string> Load()
	{
		if (!File.Exists(FilePath))
			return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(FilePath));
		return dict is not null
			? new Dictionary<string, string>(dict, StringComparer.OrdinalIgnoreCase)
			: new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
	}

	private static void Save(Dictionary<string, string> aliases)
	{
		Directory.CreateDirectory(FolderPath);
		File.WriteAllText(FilePath, JsonSerializer.Serialize(aliases, JsonSerializerOptions));
	}
}
