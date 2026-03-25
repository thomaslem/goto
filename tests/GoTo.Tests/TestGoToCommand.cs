using GoTo.Commands;
using GoTo.Data;
using GoTo.Shell;

using Spectre.Console;

namespace GoTo.Tests;

public class TestGoToCommand(
    IAnsiConsole? console = null,
    IAliasStore? aliasStore = null,
    IShellProfile? profileWriter = null)
    : GoToCommand(
        console ?? TestConsole.Create(),
        aliasStore ?? new FakeAliasStore(),
        profileWriter ?? new FakeShellProfile());