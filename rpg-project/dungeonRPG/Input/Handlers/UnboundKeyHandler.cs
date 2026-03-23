using dungeonRPG.Dungeon;
using dungeonRPG.Entities;

namespace dungeonRPG.Input.Handlers;

public class UnboundKeyHandler : BaseInputHandler
{
    public override InputResult HandleInput(ConsoleKey key, Player player, Room room, List<ConsoleKey> activeKeys)
    {
        return new InputResult(false, "Unknown command!");
    }
}