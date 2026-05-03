using System.CommandLine;

using GoTo.Data;

using Spectre.Console;

namespace GoTo.Commands;

public sealed class ListCommand : Command
{
	public ListCommand(IAnsiConsole console, IAliasStore aliasStore) : base("list", "List all directory aliases")
	{
		SetAction(_ =>
		{
			var aliases = aliasStore.GetAll();

			if (!aliases.Any())
			{
				console.MarkupLine("[yellow]No aliases defined.[/]");
				return 0;
			}
			
			var table = new Table()
				.AddColumn("Alias")
				.AddColumn("Path")
				.HideHeaders()
				.NoBorder();

			foreach ((string alias, string path) in aliases)
			{
				table.AddRow($"[cyan]{Markup.Escape(alias)}[/]", Markup.Escape(path));
			}

			console.Write(table);
			return 0;
		});
	}
}
