using GoTo.Commands;

namespace GoTo.Tests.Commands;

public class GoToCommandTests
{
    [Test]
    public async Task HelpOption()
    {
        await new GoToCommand(TestConsole.Create(), new FakeAliasStore())
            .Parse("--help")
            .InvokeAsync();

        await Verify(TestConsole.GetOutput());
    }

    [Test]
    public async Task QuestionMarkOption()
    {
        await new GoToCommand(TestConsole.Create(), new FakeAliasStore())
            .Parse("-?")
            .InvokeAsync();

        await Verify(TestConsole.GetOutput());
    }
}
