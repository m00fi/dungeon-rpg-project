using dungeonRPG.Dungeon.Generation.Strategies;

namespace dungeonRPG.UI;

public class DungeonSelectionMenu
{
    public IDungeonGenerationStrategy Display()
    {
        Console.Clear();
        string logo = "'Rogue (1980)'-like Dungeon RPG game, Michał Chrostowski\n";
    
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(logo);
        Console.ResetColor();
        
        Console.WriteLine("Select a dungeon type:");
        Console.WriteLine("1. Default Terrain");
        Console.WriteLine("2. Boss Arena");
        Console.WriteLine("3. Random Room Maze");
        
        var choice = Console.ReadKey(true).Key;
        IDungeonGenerationStrategy strategy = new DefaultTerrainStrategy();

        switch (choice)
        {
            case ConsoleKey.D1:
                break;
            case ConsoleKey.D2: 
                strategy = new BossArenaStrategy(); 
                break;
            case ConsoleKey.D3: 
                strategy = new RandomRoomMazeStrategy(); 
                break;
        }

        return strategy;
    }
}