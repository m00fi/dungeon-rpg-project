using dungeonRPG.Dungeon;
using dungeonRPG.Entities;
using dungeonRPG.Logging;

namespace dungeonRPG.Input.Handlers;

public class ExitGameHandler : BaseInputHandler
{
    public override InputResult HandleInput(ConsoleKey key, Player player, Room room, List<ConsoleKey> activeKeys)
    {
        switch (key)
        {
            case ConsoleKey.Escape:
                return new InputResult(true);
        }

        if (player.stats.Health <= 0)
        {
            string fileName = GameLogger.CurrentLogFilePath ?? "unknown";
            return new InputResult(false, $"YOU DIED! GAME OVER. Logs saved to: {fileName}. Press [ESC] to quit.");
        }
        
        return base.HandleInput(key, player, room, activeKeys);
    }
}