namespace GoTo.Tests.Commands;

public class GoCommandTests
{
    [Test]
    public async Task HelpOption()
    {
        await new TestGoToCommand()
            .Parse(["go", "--help"])
            .InvokeAsync();

        await Verify(TestConsole.GetOutput());
    }

    [Test]
    public async Task NonExistingAlias()
    {
        await new TestGoToCommand()
            .Parse(["go", "test"])
            .InvokeAsync();

        await Verify(TestConsole.GetOutput());
    }

    [Test]
    public async Task ExistingAlias()
    {
        await new TestGoToCommand(aliasStore: new FakeAliasStore(new Dictionary<string, string>
            {
                { "test", "/home/test" }
            }))
            .Parse(["go", "test"])
            .InvokeAsync();

        await Verify(TestConsole.GetOutput());
    }
}