using Spectre.Console;

namespace GoTo.Tests;

public static class TestConsole
{
	public static IAnsiConsole Create()
	{
		var console = AnsiConsole.Create(new AnsiConsoleSettings
		{
			Out = new AnsiConsoleOutput(TestContext.Current!.OutputWriter),
			ColorSystem = ColorSystemSupport.NoColors,
			Interactive = InteractionSupport.No
		});
		console.Profile.Width = 1000; // Prevent newlines in test results
		return console;
	}

	public static string GetOutput()
	{
		var error = TestContext.Current!.Output.GetErrorOutput();
		var standard = TestContext.Current.Output.GetStandardOutput();
		return error + (string.IsNullOrWhiteSpace(error) ? string.Empty : "\n") + standard;
	}
}
