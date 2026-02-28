using System.CommandLine;

using GoTo.Data;

using Spectre.Console;

namespace GoTo.Commands;

public sealed class GoToCommand : RootCommand
{
    private readonly Argument<string?> _alias = new("alias")
    {
        Arity = ArgumentArity.ZeroOrOne,
        Description = "Directory alias to navigate to"
    };
    
    public GoToCommand(IAnsiConsole console, IAliasStore aliasStore)
    {
        Arguments.Add(_alias);
        Subcommands.Add(new AddCommand(aliasStore));
        
        SetAction(result =>
        {
            var alias = result.GetRequiredValue(_alias);

            if (string.IsNullOrWhiteSpace(alias))
                return 1;

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
