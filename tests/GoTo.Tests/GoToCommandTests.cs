using GoTo.Commands;

namespace GoTo.Tests;

[NotInParallel]
public class GoToCommandTests
{
    [Test]
    public async Task HelpOption()
    {
        await new GoToCommand().Parse("--help").InvokeAsync();
        await Verify(TestConsole.GetTestOutput());
    }
}
