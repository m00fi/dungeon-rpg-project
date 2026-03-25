using dungeonRPG.Dungeon.Generation.Strategies;

namespace dungeonRPG.UI;

public class DungeonSelectionMenu
{
    public IDungeonGenerationStrategy Display()
    {
        Console.CursorVisible = false;
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
        Console.WriteLine("4. Field with Items");
        Console.WriteLine("5. Random Corridor Maze");
        
        var choice = Console.ReadKey(true).Key;
        IDungeonGenerationStrategy strategy = new RandomRoomMazeStrategy();

        switch (choice)
        {
            case ConsoleKey.D1:
                strategy = new DefaultTerrainStrategy(); 
                break;
            case ConsoleKey.D2: 
                strategy = new BossArenaStrategy(); 
                break;
            case ConsoleKey.D3: 
                break;
            case ConsoleKey.D4:
                strategy = new FieldWithItemsStrategy();
                break;
            case ConsoleKey.D5:
                strategy = new RandomCorridorMazeStrategy();
                break;
        }

        return strategy;
    }
}