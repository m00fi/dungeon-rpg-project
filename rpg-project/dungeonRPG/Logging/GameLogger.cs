namespace dungeonRPG.Logging;

public static class GameLogger
{
    private static ILogger _currentLogger = new MemoryLogger(); 

    public static void Initialize(ILogger logger)
    {
        _currentLogger = logger;
    }

    public static void Log(string message)
    {
        _currentLogger.Log(message);
    }

    public static List<string> GetRecentLogs(int count) => _currentLogger.GetRecentLogs(count);
    public static List<string> GetAllLogs() => _currentLogger.GetAllLogs();
}