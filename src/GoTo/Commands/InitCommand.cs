using System.CommandLine;

using GoTo.Shell;

using Spectre.Console;

namespace GoTo.Commands;

public sealed class InitCommand : Command
{
	private readonly Option<ShellType?> _shell = new("--shell", "-s")
	{
		Description = "Shell type, auto-detected if omitted"
	};

	private readonly Option<bool> _print = new("--print", "-p")
	{
		Description = "Print output to stdout instead of shell profile script"
	};

	public InitCommand(IAnsiConsole console, IShellProfile shellProfile)
		: base("init", "Install the gt shell function into your shell profile")
	{
		Options.Add(_shell);
		Options.Add(_print);

		SetAction(result =>
		{
			var shell = result.GetValue(_shell) ?? ShellDetector.Detect();

			if (shell is null)
			{
				console.MarkupLine($"[red]Error: could not detect shell. Use --shell {ShellTypesHint()}.[/]");
				return 1;
			}

			if (result.GetValue(_print))
			{
				console.WriteLine(ShellFunctions.Get(shell.Value));
				return 0;
			}

			var profilePath = shellProfile.GetPath(shell.Value);
			var existing = shellProfile.Read(profilePath);

			if (existing is not null && existing.Contains(ShellFunctions.Marker))
			{
				console.MarkupLineInterpolated($"[yellow]Already installed:[/] {profilePath}");
				return 0;
			}

			shellProfile.Append(profilePath, ShellFunctions.Get(shell.Value));

			console.MarkupLineInterpolated($"[green]Installed:[/] gt → {profilePath}");
			console.MarkupLineInterpolated($"[grey]Run:[/] source {profilePath}");
			return 0;
		});
	}

	private static string ShellTypesHint() =>
		string.Join('|', Enum.GetValues<ShellType>().Select(s => s.ToString().ToLower()));
}
