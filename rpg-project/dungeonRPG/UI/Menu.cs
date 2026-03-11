namespace dungeonRPG.UI;

public class Menu
{
    public string DisplayMenu()
    {
        Console.Clear();
        string logo = "'Rogue (1980)'-like Dungeon RPG game" + ", Michał Chrostowski\n";
    
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(logo);
        Console.ResetColor();
        
        Console.WriteLine("Controls:");
        Console.WriteLine(" [WASD]\t- Move");
        Console.WriteLine(" [↑/↓]\t- Select item in inventory/pickup list");
        Console.WriteLine(" [E]\t- Pick up item");
        Console.WriteLine(" [Q]\t- Drop item");
        Console.WriteLine(" [I]\t- Switch between inventory/pickup mode");
        Console.WriteLine(" [T]\t- Use (equip) selected item");
        Console.WriteLine(" [Y]\t- Unequip items from hands");
        Console.WriteLine(" [ESC]\t- Exit game");
        Console.WriteLine();
        
        Console.Write("Press [ENTER] to enter the dungeon...\nName (max 10): ");
        
        var hero = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(hero) || hero.Length > 10)
        {
            return "m0fi";
        }
        
        return hero;
    }
}