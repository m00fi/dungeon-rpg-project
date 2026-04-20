namespace dungeonRPG.Logging;

public class MemoryLogger : ILogger
{
    private readonly List<string> _logs = new List<string>();

    public void Log(string message)
    {
        string timestamp = DateTime.Now.ToString("HH:mm:ss");
        _logs.Add($"[{timestamp}] {message}");
    }

    public List<string> GetRecentLogs(int count)
    {
        return _logs.TakeLast(count).ToList();
    }

    public List<string> GetAllLogs()
    {
        return new List<string>(_logs);
    }
}