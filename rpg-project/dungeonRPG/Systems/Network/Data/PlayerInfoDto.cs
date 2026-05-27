namespace dungeonRPG.Systems.Network.Data;

public class PlayerInfoDto
{
    public int Id { get; set; }
    public char Symbol { get; set; }
    public string Name { get; set; } = "";
    public int X { get; set; }
    public int Y { get; set; }

    public List<string> StatLines { get; set; } = new();
    public string MoneyLine { get; set; } = "";

    public string LeftHandDescription { get; set; } = "";
    public string RightHandDescription { get; set; } = "";

    public List<string> InventoryItems { get; set; } = new();
    public int InventoryCapacity { get; set; }

    public List<string> GroundItems { get; set; } = new();

    public bool IsInventoryActive { get; set; }
    public int SelectedInventoryIndex { get; set; }
    public int SelectedItemIndex { get; set; }
    public bool IsInstructionsOpen { get; set; }
    public bool IsLogsOpen { get; set; }

    public bool IsInCombat { get; set; }
    public EnemyInfoDto? ActiveEnemy { get; set; }
}
