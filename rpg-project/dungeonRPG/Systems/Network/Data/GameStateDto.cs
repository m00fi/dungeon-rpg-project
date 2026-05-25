namespace dungeonRPG.Systems.Network.Data;

public class GameStateDto
{
    // Pre-rendered map: 20 rows, each 40 chars wide (enemies, items — no players)
    public string[] MapRows { get; set; } = Array.Empty<string>();

    // All connected players keyed by slot id (1-9)
    public Dictionary<int, PlayerInfoDto> Players { get; set; } = new();

    // Recent entries from MemoryLogger for the event log panel
    public List<string> RecentLogs { get; set; } = new();

    // Full log history, sent when any player has IsLogsOpen = true
    public List<string> AllLogs { get; set; } = new();

    // Dungeon instructions (key bindings list shown on 'I')
    public List<string> Instructions { get; set; } = new();
}
