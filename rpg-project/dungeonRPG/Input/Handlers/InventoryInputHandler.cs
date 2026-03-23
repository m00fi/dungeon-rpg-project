using dungeonRPG.Dungeon;
using dungeonRPG.Entities;

namespace dungeonRPG.Input.Handlers;

public class InventoryInputHandler : BaseInputHandler
{
    public override InputResult HandleInput(ConsoleKey key, Player player, Room room, List<ConsoleKey> activeKeys)
    {
        if (!player.IsInventoryActive)
        {
            return base.HandleInput(key, player, room, activeKeys);
        }
        
        if(!activeKeys.Contains(key))
            return base.HandleInput(key, player, room, activeKeys);
        string? result = null;
        switch (key)
        {
            case ConsoleKey.UpArrow: 
                player.SelectPreviousItem(); 
                return new InputResult();
                
            case ConsoleKey.DownArrow: 
                player.SelectNextItem(room); 
                return new InputResult();
                
            case ConsoleKey.E: 
                result = player.TryUseItem(room);
                return new InputResult(false, result);
                
            case ConsoleKey.Q: 
                result = player.TryDropItem(room); 
                return new InputResult(false, result);
                
            default:
                return base.HandleInput(key, player, room, activeKeys);
        }
    }
}