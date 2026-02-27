using Spectre.Console;

namespace GoTo.Tests;

public static class TestConsole
{
    public static IAnsiConsole Create() => AnsiConsole.Create(new AnsiConsoleSettings
    {
        Out = new AnsiConsoleOutput(TestContext.Current!.OutputWriter)
    });
    
    public static string GetOutput() => TestContext.Current!.Output.GetStandardOutput();
}
