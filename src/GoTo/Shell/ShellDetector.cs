namespace GoTo.Shell;

public static class ShellDetector
{
    public static ShellType? Detect()
    {
        var shell = Environment.GetEnvironmentVariable("SHELL");
        if (shell is not null)
        {
            if (shell.EndsWith("bash"))
                return ShellType.Bash;

            if (shell.EndsWith("zsh"))
                return ShellType.Zsh;
        }

        if (Environment.GetEnvironmentVariable("PSModulePath") is not null)
            return ShellType.PowerShell;

        return null;
    }
}