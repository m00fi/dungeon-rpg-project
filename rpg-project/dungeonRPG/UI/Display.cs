namespace dungeonRPG.UI;

using Dungeon;
using Entities;

public class Display
{
    public void Render(Room room, Player player)
    {
        Console.SetCursorPosition(0, 0);
        // Console.BackgroundColor = ConsoleColor.Black;
        for (int y = 0; y < Room.Height; y++)
        {
            for (int x = 0; x < Room.Width; x++)
            {
                if(x == player.X && y == player.Y)
                {
                    Console.Write('¶');
                    continue;
                }
                Console.Write(room.GetCell(x, y).GetSymbol());
            }
            Console.WriteLine();
        }
    }
    
}