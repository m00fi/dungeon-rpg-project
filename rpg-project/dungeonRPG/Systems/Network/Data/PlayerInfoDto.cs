namespace dungeonRPG.Systems.Network.Data;

public class PlayerInfoDto
{
    public int Id { get; set; }
    public char Symbol { get; set; }
    public string Name { get; set; } = "";
    public int X { get; set; }
    public int Y { get; set; }

    // Stats as formatted strings (mirrors Attribute.GetAttributes())
    public List<string> StatLines { get; set; } = new();
    public string MoneyLine { get; set; } = "";

    // Equipment slot descriptions
    public string LeftHandDescription { get; set; } = "";
    public string RightHandDescription { get; set; } = "";

    // Inventory as text descriptions (no IItem references)
    public List<string> InventoryItems { get; set; } = new();
    public int InventoryCapacity { get; set; }

    // Ground items at player's current position
    public List<string> GroundItems { get; set; } = new();

    // UI state
    public bool IsInventoryActive { get; set; }
    public int SelectedInventoryIndex { get; set; }
    public int SelectedItemIndex { get; set; }
    public bool IsInstructionsOpen { get; set; }
    public bool IsLogsOpen { get; set; }

    // Combat state
    public bool IsInCombat { get; set; }
    public EnemyInfoDto? ActiveEnemy { get; set; }
}
