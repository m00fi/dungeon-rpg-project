using dungeonRPG.Dungeon;
using dungeonRPG.Entities;

namespace dungeonRPG.Input.Handlers;

public class ExitGameHandler : BaseInputHandler
{
    public override InputResult HandleInput(ConsoleKey key, Player player, Room room, List<ConsoleKey> activeKeys)
    {
        if (player.stats.Health <= 0)
            return new InputResult(true);
        
        switch (key)
        {
            case ConsoleKey.Escape:
                return new InputResult(true);
            default:
                return base.HandleInput(key, player, room, activeKeys);
        }
    }
}