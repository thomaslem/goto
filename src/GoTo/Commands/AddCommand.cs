using System.CommandLine;

using GoTo.Data;

using Spectre.Console;

namespace GoTo.Commands;

public sealed class AddCommand : Command
{
    private readonly Argument<string> _alias = new ("alias")
    {
        Description = "Name of the alias"
    };
    
    private readonly Argument<string?> _path = new("path")
    {
        Arity = ArgumentArity.ZeroOrOne,
        Description = "Directory path (defaults to current directory)"
    };
    
    public AddCommand(IAliasStore aliasStore) : base("add", "Save an alias for a directory")
    {
        Arguments.Add(_alias);
        Arguments.Add(_path);
        
        SetAction(result =>
        {
            var alias = result.GetRequiredValue(_alias);
            var path = result.GetValue(_path);
            
            var fullPath = path is not null
                ? Path.GetFullPath(path)
                : Directory.GetCurrentDirectory();

            if (!Directory.Exists(fullPath))
            {
                AnsiConsole.MarkupLineInterpolated($"[red]Error: directory does not exist: {fullPath}[/]");
                return 1;
            }

            aliasStore.Add(alias, fullPath);
            AnsiConsole.MarkupLineInterpolated($"[green]Added:[/] [cyan]{alias}[/] → {fullPath}");
            return 0;
        });
    }
}
