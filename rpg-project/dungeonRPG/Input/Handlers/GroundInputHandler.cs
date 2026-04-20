using dungeonRPG.Dungeon;
using dungeonRPG.Entities;

namespace dungeonRPG.Input.Handlers;

public class GroundInputHandler : BaseInputHandler
{
    public override InputResult HandleInput(ConsoleKey key, Player player, Room room, List<ConsoleKey> activeKeys)
    {
        if (player.IsInventoryActive)
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
                result = player.TryPickUp(room);
                return new InputResult(false, result);
            
            case ConsoleKey.Y: 
                return new InputResult(false, $"Open inventory [{InventoryKeybinds.ToggleInventoryInfo}] to unequip items.");
            
            case ConsoleKey.Q:
                return new InputResult(false, $"Press [{InventoryKeybinds.ToggleInventoryInfo}] to open inventory and select item to drop.");
                
            default:
                return base.HandleInput(key, player, room, activeKeys);
        }
    }
}