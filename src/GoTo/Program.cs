using GoTo.Commands;

var rootCommand = new GoToCommand();

return args.Length == 0
    ? await rootCommand.Parse("--help").InvokeAsync()
    : await rootCommand.Parse(args).InvokeAsync();
