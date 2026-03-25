using GoTo.Commands;
using GoTo.Shell;

namespace GoTo.Tests.Commands;

public class InitCommandTests
{
	[Test]
	public async Task HelpOption()
	{
		await new GoToCommand(TestConsole.Create(), new FakeAliasStore(), new FakeShellProfile())
			.Parse(["init", "--help"])
			.InvokeAsync();

		await Verify(TestConsole.GetOutput());
	}

	[Test]
	public async Task ExplicitShellBash()
	{
		await new TestGoToCommand()
			.Parse(["init", "--shell", "bash"])
			.InvokeAsync();

		await Verify(TestConsole.GetOutput());
	}

	[Test]
	public async Task ExplicitShellZsh()
	{
		await new TestGoToCommand()
			.Parse(["init", "--shell", "zsh"])
			.InvokeAsync();

		await Verify(TestConsole.GetOutput());
	}

	[Test]
	public async Task ExplicitShellPowerShell()
	{
		await new TestGoToCommand()
			.Parse(["init", "--shell", "powershell"])
			.InvokeAsync();

		await Verify(TestConsole.GetOutput());
	}

	[Test]
	public async Task AlreadyInstalled()
	{
		var writer = new FakeShellProfile(ShellFunctions.Marker);

		await new TestGoToCommand(profileWriter: writer)
			.Parse(["init", "--shell", "bash"])
			.InvokeAsync();

		await Verify(TestConsole.GetOutput());
	}

	[Test]
	public async Task UnknownShell()
	{
		await new TestGoToCommand()
			.Parse(["init", "--shell", "fish"])
			.InvokeAsync();

		await Verify(TestConsole.GetOutput());
	}
}
