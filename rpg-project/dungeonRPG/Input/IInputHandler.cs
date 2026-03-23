using dungeonRPG.Dungeon;
using dungeonRPG.Entities;

namespace dungeonRPG.Input;

public interface IInputHandler
{
    IInputHandler SetNext(IInputHandler handler);
    InputResult HandleInput(ConsoleKey key, Player player, Room room, List<ConsoleKey> activeKeys);
}