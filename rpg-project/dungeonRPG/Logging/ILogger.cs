namespace dungeonRPG.Logging;

public interface ILogger
{
    string? LogFilePath { get; }
    void Log(string message);
    List<string> GetRecentLogs(int count);
    List<string> GetAllLogs();
}