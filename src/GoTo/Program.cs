using GoTo.Commands;
using GoTo.Data;
using GoTo.Shell;

using Spectre.Console;

var rootCommand = new GoToCommand(AnsiConsole.Console, new AliasStore(), new ShellProfile());

return args.Length == 0
    ? await rootCommand.Parse("--help").InvokeAsync()
    : await rootCommand.Parse(args).InvokeAsync();