namespace dungeonRPG.UI;

public class Menu
{
    List<string> _instructions;

    public Menu(List<string> instructions)
    {
        _instructions = instructions;
    }
    public void Display()
    {
        Console.Clear();
        string logo = "'Rogue (1980)'-like Dungeon RPG game, Michał Chrostowski\n";
    
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(logo);
        Console.ResetColor();
        
        Console.WriteLine("Controls:");
        if (_instructions.Count == 0)
        {
            Console.WriteLine(" [WASD]\t- Move");
            Console.WriteLine(" [↑/↓]\t- Select item in inventory/pickup list");
            Console.WriteLine(" [E]\t- Pick up / use (equip) selected item");
            Console.WriteLine(" [Q]\t- Drop item");
            Console.WriteLine(" [I]\t- Switch between inventory/pickup mode");
            Console.WriteLine(" [Y]\t- Unequip items from hands");
            Console.WriteLine(" [ESC]\t- Exit game");
        }
        else
        {
            foreach (var i in _instructions)
            {
                Console.WriteLine(i);
            }
        }
        
        Console.WriteLine();
        
        Console.Write("Press [ENTER] to enter the dungeon...");
        while (true)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Enter)
                break;
        }
    }
    
}