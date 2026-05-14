using System.CommandLine;
using System.CommandLine.Completions;

using GoTo.Data;

using Spectre.Console;

namespace GoTo.Commands;

public sealed class RemoveCommand : Command
{
	private readonly Argument<string> _alias = new("alias")
	{
		Description = "Name of the alias to remove"
	};

	private readonly Option<bool> _yes = new("--yes", "-y")
	{
		Description = "Skip confirmation prompt"
	};

	public RemoveCommand(IAnsiConsole console, IAliasStore aliasStore) : base("remove", "Remove a directory alias")
	{
		_alias.CompletionSources.Add(_ => aliasStore.GetAll().Keys.Select(alias => new CompletionItem(alias)));

		Arguments.Add(_alias);
		Options.Add(_yes);

		SetAction(result =>
		{
			var alias = result.GetRequiredValue(_alias);
			var yes = result.GetValue(_yes);

			var path = aliasStore.Search(alias);
			if (path is null)
			{
				console.MarkupLineInterpolated($"[red]Error: Alias '{alias}' not found[/]");
				return 1;
			}

			if (!yes)
			{
				if (!console.Profile.Capabilities.Interactive)
				{
					console.MarkupLine("[red]Error: Use -y/--yes to remove in non-interactive mode[/]");
					return 1;
				}

				if (!console.Confirm($"[red]Remove[/] '[cyan]{alias}[/]' → {path}?", defaultValue: false))
				{
					console.MarkupLine("[yellow]Cancelled.[/]");
					return 0;
				}
			}

			aliasStore.Remove(alias);
			console.MarkupLineInterpolated($"[red]Removed:[/] [cyan]{alias}[/]");
			return 0;
		});
	}
}
