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

	public RemoveCommand(IAnsiConsole console, IAliasStore aliasStore) : base("remove", "Remove a directory alias")
	{
		_alias.CompletionSources.Add(_ => aliasStore.GetAll().Keys.Select(alias => new CompletionItem(alias)));

		Arguments.Add(_alias);

		SetAction(result =>
		{
			var alias = result.GetRequiredValue(_alias);

			if (aliasStore.Get(alias) is null)
			{
				console.MarkupLineInterpolated($"[red]Error: Alias '{alias}' not found[/]");
				return 1;
			}

			aliasStore.Remove(alias);
			console.MarkupLineInterpolated($"[red]Removed:[/] [cyan]{alias}[/]");
			return 0;
		});
	}
}
