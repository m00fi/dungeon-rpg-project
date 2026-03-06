namespace dungeonRPG;

using Dungeon;
using UI;
public class Game
{
    private readonly Room _room;
    private readonly Display _display;

    public Game()
    {
        _room = new Room();
        _display = new Display();
    }

    public void Run()
    {
        Console.CursorVisible = false;
        Console.Clear();

        try
        {
            _display.Render(_room);
            Console.ReadKey();
        }
        finally
        {
            Console.CursorVisible = true; // zawsze przywróć kursor
            Console.Clear();
        }
    }
}