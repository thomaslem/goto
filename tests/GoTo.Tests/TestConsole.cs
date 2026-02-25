using System.Collections.Concurrent;

using TUnit.Core.Logging;

namespace GoTo.Tests;

public sealed class TestConsole : ILogSink
{
    public static readonly TestConsole Instance = new();

    private readonly ConcurrentDictionary<string, List<string>> _testLogs = new();

    public bool IsEnabled(LogLevel level) => true;

    public void Log(LogLevel level, string message, Exception? exception, Context? context)
    {
        if (context is not TestContext testContext)
            return;
        
        _testLogs.GetOrAdd(testContext.Id, _ => [])
            .Add(message);
    }

    public ValueTask LogAsync(LogLevel level, string message, Exception? exception, Context? context)
    {
        Log(level, message, exception, context);
        return ValueTask.CompletedTask;
    }

    public static string GetTestOutput() =>
        TestContext.Current is not null && Instance._testLogs.TryGetValue(TestContext.Current.Id, out var logs)
            ? string.Join('\n', logs)
            : string.Empty;
}
