using dungeonRPG.Dungeon;
using dungeonRPG.Entities;

namespace dungeonRPG.Input;

public abstract class BaseInputHandler : IInputHandler
{
    private IInputHandler? _nextInputHandler;

    public IInputHandler SetNext(IInputHandler handler)
    {
        _nextInputHandler = handler;
        return handler;
    }
    
    public virtual InputResult HandleInput(ConsoleKey key, Player player, Room room)
    {
        if (_nextInputHandler != null)
        {
            return _nextInputHandler.HandleInput(key, player, room);
        }
        
        return new InputResult(false, "Unknown command!");
    }
}