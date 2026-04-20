using dungeonRPG.Dungeon;
using dungeonRPG.Entities;
using dungeonRPG.Logging;

namespace dungeonRPG.Input.Handlers;

public class UnboundKeyHandler : BaseInputHandler
{
    public override InputResult HandleInput(ConsoleKey key, Player player, Room room, List<ConsoleKey> activeKeys)
    {
        GameLogger.Log("Player pressed an unbound key.");
        return new InputResult(false, "Unknown command!");
    }
}