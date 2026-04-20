namespace dungeonRPG.Logging;

public class FileLogger : ILogger
{
    public string LogFilePath {get; private set;}

    public FileLogger(string directoryPath, string playerName)
    {
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string fileName = $"{playerName}_{timestamp}.log";
        
        LogFilePath = Path.Combine(directoryPath, fileName);
        File.WriteAllText(LogFilePath, $"---DUNGEON-RPG FILE LOG FOR: {playerName}---\n");
    }
    
    public void Log(string message)
    {
        string timestamp = DateTime.Now.ToString("HH:mm:ss");
        File.AppendAllText(LogFilePath, $"[{timestamp}] {message}\n");
    }
    public List<string> GetRecentLogs(int count) => new List<string>();
    public List<string> GetAllLogs() => new List<string>();
}