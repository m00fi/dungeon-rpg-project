using dungeonRPG.Dungeon;
using dungeonRPG.Entities;

namespace dungeonRPG.Input.Handlers;

public class CombatInputHandler : BaseInputHandler
{
    public override InputResult HandleInput(ConsoleKey key, Player player, Room room, List<ConsoleKey> activeKeys)
    {
        if (!player.IsInCombat)
        {
            return base.HandleInput(key, player, room, activeKeys);
        }

        switch (key)
        {
            case ConsoleKey.D1:
                return new InputResult(false, "You used Normal Attack! (To be implemented)");
                
            case ConsoleKey.D2:
                return new InputResult(false, "You used Stealth Attack! (To be implemented)");
                
            case ConsoleKey.D3:
                return new InputResult(false, "You used Magic Attack! (To be implemented)");

            default:
                return new InputResult(false, "You cannot do this while in combat!");
        }
    }
}