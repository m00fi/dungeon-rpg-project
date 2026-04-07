namespace dungeonRPG.Input;

public class KeyBinds
{
    public static readonly Dictionary<GameAction, ConsoleKey> Map = new()
    {
        { GameAction.MoveUp, ConsoleKey.W },
        { GameAction.MoveDown, ConsoleKey.S },
        { GameAction.MoveLeft, ConsoleKey.A },
        { GameAction.MoveRight, ConsoleKey.D },
        { GameAction.Interact, ConsoleKey.E },
        { GameAction.DropItem, ConsoleKey.Q },
        { GameAction.ToggleInventory, ConsoleKey.I },
        { GameAction.UnequipAll, ConsoleKey.Y },
        { GameAction.SelectNext, ConsoleKey.DownArrow },
        { GameAction.SelectPrev, ConsoleKey.UpArrow },
        { GameAction.ExitGame, ConsoleKey.Escape }
    };

    public static GameAction? GetAction(ConsoleKey key)
    {
        foreach (var kvp in Map)
        {
            if (kvp.Value == key) return kvp.Key;
        }
        return null;
    }
}