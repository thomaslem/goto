using TUnit.Core.Logging;

namespace GoTo.Tests;

public class AssemblyInitializer
{
    [Before(Assembly)]
    public static void Initialize()
    {
        TUnitLoggerFactory.AddSink(TestConsole.Instance);
    }
}