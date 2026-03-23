using dungeonRPG.Dungeon;
using dungeonRPG.Entities;

namespace dungeonRPG.Input.Handlers;

public class MovementInputHandler : BaseInputHandler
{
    public override InputResult HandleInput(ConsoleKey key, Player player, Room room, List<ConsoleKey> activeKeys)
    {
        if(!activeKeys.Contains(key))
            return base.HandleInput(key, player, room, activeKeys);
        
        
        switch (key)
        {
            case ConsoleKey.W: 
                player.TryMove(0, -1, room); 
                return new InputResult();
            case ConsoleKey.S: 
                player.TryMove(0, 1, room); 
                return new InputResult();
            case ConsoleKey.A: 
                player.TryMove(-1, 0, room);
                player.Symbol = '¶';
                return new InputResult();
            case ConsoleKey.D: 
                player.TryMove(1, 0, room);
                player.Symbol = '⁋';
                return new InputResult();
            
            default: return base.HandleInput(key, player, room, activeKeys);
        }
    }
}