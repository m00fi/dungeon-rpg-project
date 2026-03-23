using dungeonRPG.Dungeon;
using dungeonRPG.Entities;

namespace dungeonRPG.Input.Handlers;

public class GlobalActionHandler : BaseInputHandler
{
    public override InputResult HandleInput(ConsoleKey key, Player player, Room room, List<ConsoleKey> activeKeys)
    {
        if(!activeKeys.Contains(key))
            return base.HandleInput(key, player, room, activeKeys);
        
        switch (key)
        {
            case ConsoleKey.I: 
                player.ToggleInventory(); 
                return new InputResult();
                
            case ConsoleKey.Y: 
                player.TryUnequipAll(room); 
                return new InputResult();
                
            case ConsoleKey.Escape: 
                return new InputResult(true);
                
            default: 
                return base.HandleInput(key, player, room, activeKeys);
        }
    }
}