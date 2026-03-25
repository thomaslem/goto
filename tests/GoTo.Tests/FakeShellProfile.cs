using GoTo.Shell;

namespace GoTo.Tests;

public class FakeShellProfile(string? existingContent = null) : IShellProfile
{
    public string? Read(string path) =>
        existingContent;

    public void Append(string path, string content) { }

    public string GetPath(ShellType shell) => "/home/test/.profile";
}