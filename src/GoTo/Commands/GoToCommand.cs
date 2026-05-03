using System.CommandLine;

using GoTo.Data;
using GoTo.Shell;

using Spectre.Console;

namespace GoTo.Commands;

public class GoToCommand : RootCommand
{
	private readonly Option<bool> _commands = new("--list-commands")
	{
		Hidden = true
	};

	public GoToCommand(IAnsiConsole console, IAliasStore aliasStore, IShellProfile shellProfile)
	{
		Options.Add(_commands);

		Subcommands.Add(new GetCommand(console, aliasStore));
		Subcommands.Add(new AddCommand(console, aliasStore));
		Subcommands.Add(new ListCommand(console, aliasStore));
		Subcommands.Add(new InitCommand(console, shellProfile));

		SetAction(result =>
		{
			if (!result.GetValue(_commands))
				return 0;

			foreach (var command in Subcommands)
			{
				console.WriteLine(command.Name);
			}

			return 0;
		});
	}
}
