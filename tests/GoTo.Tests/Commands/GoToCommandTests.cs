namespace GoTo.Tests.Commands;

public class GoToCommandTests
{
	[Test]
	public async Task HelpOption()
	{
		await new TestGoToCommand()
			.Parse("--help")
			.InvokeAsync();

		await Verify(TestConsole.GetOutput());
	}

	[Test]
	public async Task QuestionMarkOption()
	{
		await new TestGoToCommand()
			.Parse("-?")
			.InvokeAsync();

		await Verify(TestConsole.GetOutput());
	}

	[Test]
	public async Task SubcommandsOption()
	{
		await new TestGoToCommand()
			.Parse("--list-commands")
			.InvokeAsync();

		await Verify(TestConsole.GetOutput());
	}
}
