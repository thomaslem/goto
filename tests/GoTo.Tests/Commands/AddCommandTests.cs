namespace GoTo.Tests.Commands;

public class AddCommandTests
{
    [Test]
    public async Task HelpOption()
    {
        await new TestGoToCommand()
            .Parse(["add", "--help"])
            .InvokeAsync();

        await Verify(TestConsole.GetOutput());
    }

    [Test]
    public async Task NonExistingPath()
    {
        var path = Path.Combine(Path.GetTempPath(), "goto-test-nonexistent");

        await new TestGoToCommand()
            .Parse(["add", "test", path])
            .InvokeAsync();

        var settings = new VerifySettings();
        settings.ScrubLinesWithReplace(line => line.Replace(path, "{path}"));
        await Verify(TestConsole.GetOutput(), settings);
    }

    [Test]
    public async Task ExistingPath()
    {
        var dir = Directory.CreateTempSubdirectory("goto-test");
        try
        {
            await new TestGoToCommand()
                .Parse(["add", "test", dir.FullName])
                .InvokeAsync();

            var settings = new VerifySettings();
            settings.ScrubLinesWithReplace(line => line.Replace(dir.FullName, "{path}"));
            await Verify(TestConsole.GetOutput(), settings);
        }
        finally
        {
            dir.Delete();
        }
    }

    [Test]
    public async Task WithoutPath()
    {
        var currentDir = Directory.GetCurrentDirectory();

        await new TestGoToCommand()
            .Parse(["add", "test"])
            .InvokeAsync();

        var settings = new VerifySettings();
        settings.ScrubLinesWithReplace(line => line.Replace(currentDir, "{currentDir}"));
        await Verify(TestConsole.GetOutput(), settings);
    }
}