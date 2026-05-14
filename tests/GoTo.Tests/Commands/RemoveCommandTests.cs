namespace GoTo.Tests.Commands;

public class RemoveCommandTests
{
	[Test]
	public async Task HelpOption()
	{
		await new TestGoToCommand()
			.Parse(["remove", "--help"])
			.InvokeAsync();

		await Verify(TestConsole.GetOutput());
	}

	[Test]
	public async Task ExistingAlias()
	{
		var store = new FakeAliasStore(new Dictionary<string, string> { ["work"] = "/home/user/work" });

		await new TestGoToCommand(aliasStore: store)
			.Parse(["remove", "work"])
			.InvokeAsync();

		await Verify(TestConsole.GetOutput());
	}

	[Test]
	public async Task NonExistingAlias()
	{
		await new TestGoToCommand()
			.Parse(["remove", "does_not_exist"])
			.InvokeAsync();

		await Verify(TestConsole.GetOutput());
	}
}
