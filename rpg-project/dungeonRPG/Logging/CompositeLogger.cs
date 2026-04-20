namespace dungeonRPG.Logging;

public class CompositeLogger : ILogger
{
    private readonly ILogger _memoryLogger;
    private readonly ILogger _fileLogger;
    public string? LogFilePath => _fileLogger.LogFilePath;

    public CompositeLogger(ILogger memoryLogger, ILogger fileLogger)
    {
        _memoryLogger = memoryLogger;
        _fileLogger = fileLogger;
    }

    public void Log(string message)
    {
        _memoryLogger.Log(message);
        _fileLogger.Log(message);
    }

    public List<string> GetRecentLogs(int count) => _memoryLogger.GetRecentLogs(count);
    public List<string> GetAllLogs() => _memoryLogger.GetAllLogs();
}