using dungeonRPG.Dungeon.Generation.Strategies;
using dungeonRPG.Themes;

namespace dungeonRPG.UI;

public class DungeonSelectionMenu
{
    public IThemeFactory Display()
    {
        Console.CursorVisible = false;
        Console.Clear();
        string logo = "'Rogue (1980)'-like Dungeon RPG game, Michał Chrostowski\n";
    
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(logo);
        Console.ResetColor();
        
        Console.WriteLine("Select a dungeon type:");
        Console.WriteLine("1. Cursed Castle");
        Console.WriteLine("2. Crimson Catacombs");
        Console.WriteLine("3. Jungle Labyrinth");

        var choice = Console.ReadKey(true).Key;

        switch (choice)
        {
            case ConsoleKey.D1:
                return new CursedCastleThemeFactory();
            case ConsoleKey.D2:
                return new CrimsonThemeFactory();
            case ConsoleKey.D3:
                return new JungleMazeThemeFactory();
            default:
                return new CursedCastleThemeFactory();
        }
    }
}