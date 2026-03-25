namespace GoTo.Shell;

public interface IShellProfile
{
	string? Read(string path);

	void Append(string path, string content);

	public string GetPath(ShellType shell);
}

public sealed class ShellProfile : IShellProfile
{
	public string? Read(string path) =>
		File.Exists(path)
			? File.ReadAllText(path)
			: null;

	public void Append(string path, string content)
	{
		var dir = Path.GetDirectoryName(path);
		if (dir is not null)
			Directory.CreateDirectory(dir);
		File.AppendAllText(path, content);
	}

	public string GetPath(ShellType shell)
	{
		var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
		return shell switch
		{
			ShellType.Bash =>
				Path.Combine(home, ".bashrc"),

			ShellType.Zsh =>
				Path.Combine(home, ".zshrc"),

			ShellType.PowerShell =>
				OperatingSystem.IsWindows()
					? Path.Combine(home, "Documents", "PowerShell", "Microsoft.PowerShell_profile.ps1")
					: Path.Combine(home, ".config", "powershell", "Microsoft.PowerShell_profile.ps1"),

			_ => throw new ArgumentOutOfRangeException(nameof(shell))
		};
	}
}
