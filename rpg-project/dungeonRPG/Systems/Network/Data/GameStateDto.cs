namespace dungeonRPG.Systems.Network.Data;

public class GameStateDto
{
    public string[] MapRows { get; set; } = Array.Empty<string>();
    public Dictionary<int, PlayerInfoDto> Players { get; set; } = new();
    public string? CurrentMessage { get; set; }
    public List<string> RecentLogs { get; set; } = new();
    public List<string> AllLogs { get; set; } = new();
    public List<string> Instructions { get; set; } = new();
}
