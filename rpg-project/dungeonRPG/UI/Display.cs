namespace dungeonRPG.UI;

using Dungeon;

public class Display
{
    public void Render(Room room)
    {
        Console.SetCursorPosition(0, 0);
        // Console.BackgroundColor = ConsoleColor.Black;
        for (int y = 0; y < Room.Height; y++)
        {
            for (int x = 0; x < Room.Width; x++)
                Console.Write(room.GetCell(x, y).GetSymbol());
            Console.WriteLine();
        }
    }
    
}