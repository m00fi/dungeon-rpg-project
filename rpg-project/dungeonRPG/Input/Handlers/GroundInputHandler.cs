using dungeonRPG.Dungeon;
using dungeonRPG.Entities;

namespace dungeonRPG.Input.Handlers;

public class GroundInputHandler : BaseInputHandler
{
    public override InputResult HandleInput(ConsoleKey key, Player player, Room room)
    {
        // Jeśli ekwipunek jest OTWARTY, to nie nasza działka, podajemy dalej
        if (player.IsInventoryActive)
        {
            return base.HandleInput(key, player, room);
        }

        switch (key)
        {
            case ConsoleKey.UpArrow: 
                player.SelectPreviousItem(); 
                return new InputResult();
                
            case ConsoleKey.DownArrow: 
                player.SelectNextItem(room); 
                return new InputResult();
                
            case ConsoleKey.E: 
                player.TryPickUp(room);
                return new InputResult();
                
            default:
                return base.HandleInput(key, player, room);
        }
    }
}