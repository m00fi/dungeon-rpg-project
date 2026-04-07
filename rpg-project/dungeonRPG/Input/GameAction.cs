namespace dungeonRPG.Input;

public class MovementKeybinds
{
    public static readonly ConsoleKey MoveUp =  ConsoleKey.W;
    public static readonly ConsoleKey MoveDown =  ConsoleKey.S;
    public static readonly ConsoleKey MoveLeft =  ConsoleKey.A;
    public static readonly ConsoleKey MoveRight =  ConsoleKey.D;
    public static readonly ConsoleKey ExitGame = ConsoleKey.Escape;
    
    public static readonly string MovementInfo = "WASD";
    public static readonly string ExitInfo = "ESC";
}

public class InventoryKeybinds
{
    public static readonly ConsoleKey Interact = ConsoleKey.E;
    public static readonly ConsoleKey ToggleInventory = ConsoleKey.I;
    public static readonly ConsoleKey DropItem = ConsoleKey.Q;
    public static readonly ConsoleKey UnequipAll = ConsoleKey.Y;
    
    public static readonly ConsoleKey SelectNext = ConsoleKey.DownArrow;
    public static readonly ConsoleKey SelectPrev = ConsoleKey.UpArrow;

    public static readonly char InteractInfo = 'E';
    public static readonly char ToggleInventoryInfo = 'I';
    public static readonly char DropItemInfo = 'Q';
    public static readonly char UnequipAllInfo = 'Y';
    
    public static readonly string SelectInfo = "↑/↓";
}