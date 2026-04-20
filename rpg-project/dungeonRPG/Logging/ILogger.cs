namespace dungeonRPG.Logging;

public interface ILogger
{
    void Log(string message);
    List<string> GetRecentLogs(int count);
    List<string> GetAllLogs();
}