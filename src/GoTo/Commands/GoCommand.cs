using System.CommandLine;

using GoTo.Data;

using Spectre.Console;

namespace GoTo.Commands;

public sealed class GoCommand : Command
{
    private readonly Argument<string> _alias = new("alias")
    {
        Description = "Directory alias to navigate to"
    };

    public GoCommand(IAnsiConsole console, IAliasStore aliasStore) : base("go", "Navigate to a directory alias")
    {
        Arguments.Add(_alias);

        SetAction(result =>
        {
            var alias = result.GetRequiredValue(_alias);
            var path = aliasStore.Get(alias);

            if (string.IsNullOrWhiteSpace(path))
            {
                console.MarkupLineInterpolated($"[red]Error: Alias '{alias}' not found[/]");
                return 1;
            }

            console.WriteLine(path);
            return 0;
        });
    }
}