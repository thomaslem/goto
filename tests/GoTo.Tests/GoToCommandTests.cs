using GoTo.Commands;

namespace GoTo.Tests;

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
    
    [Test]
    public async Task NonExistingAlias()
    {
        await new GoToCommand(TestConsole.Create(), new FakeAliasStore())
            .Parse("test")
            .InvokeAsync();
        
        await Verify(TestConsole.GetOutput());
    }
    
    [Test]
    public async Task ExistingAlias()
    {
        await new GoToCommand(TestConsole.Create(), new FakeAliasStore(new Dictionary<string, string>
            {
                {"test", "/home/test"}
            }))
            .Parse("test")
            .InvokeAsync();
        
        await Verify(TestConsole.GetOutput());
    }
}
