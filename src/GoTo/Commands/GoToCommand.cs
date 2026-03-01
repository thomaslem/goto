using System.CommandLine;

using GoTo.Data;

using Spectre.Console;

namespace GoTo.Commands;

public sealed class GoToCommand : RootCommand
{
    public GoToCommand(IAnsiConsole console, IAliasStore aliasStore)
    {
        Subcommands.Add(new GoCommand(console, aliasStore));
        Subcommands.Add(new AddCommand(console, aliasStore));
    }
}