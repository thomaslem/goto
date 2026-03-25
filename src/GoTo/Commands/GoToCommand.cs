using System.CommandLine;

using GoTo.Data;
using GoTo.Shell;

using Spectre.Console;

namespace GoTo.Commands;

public class GoToCommand : RootCommand
{
    public GoToCommand(IAnsiConsole console, IAliasStore aliasStore, IShellProfile shellProfile)
    {
        Subcommands.Add(new GoCommand(console, aliasStore));
        Subcommands.Add(new AddCommand(console, aliasStore));
        Subcommands.Add(new InitCommand(console, shellProfile));
    }
}