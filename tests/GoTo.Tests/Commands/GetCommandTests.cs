namespace GoTo.Tests.Commands;

public class GetCommandTests
{
	[Test]
	public async Task HelpOption()
	{
		await new TestGoToCommand()
			.Parse(["get", "--help"])
			.InvokeAsync();

		await Verify(TestConsole.GetOutput());
	}

	[Test]
	public async Task NonExistingAlias()
	{
		await new TestGoToCommand()
			.Parse(["get", "test"])
			.InvokeAsync();

		await Verify(TestConsole.GetOutput());
	}

	[Test]
	public async Task ExistingAlias()
	{
		await new TestGoToCommand(aliasStore: new FakeAliasStore(new Dictionary<string, string>
			{
				{
					"test", "/home/test"
				}
			}))
			.Parse(["get", "test"])
			.InvokeAsync();

		await Verify(TestConsole.GetOutput());
	}
}
