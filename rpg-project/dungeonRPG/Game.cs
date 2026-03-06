using dungeonRPG.Dungeon.Cells;
using dungeonRPG.Entities;

namespace dungeonRPG;

using Dungeon;
using UI;
public class Game
{
    private readonly Room _room;
    private readonly Display _display;
    private readonly Player _player;

    public Game()
    {
        _room = new Room();
        _display = new Display();
        _player = new Player(1, 1);
    }

    public void Run()
    {
        Console.CursorVisible = false;
        Console.Clear();

        try
        {
            while (true)
            {
                _display.Render(_room);

                var key = Console.ReadKey(intercept: true).Key;

                switch (key)
                {
                    case ConsoleKey.W: break;
                    case ConsoleKey.S: break;
                    case ConsoleKey.A: break;
                    case ConsoleKey.D: break;
                    case ConsoleKey.Escape: return;
                }
            }
        }
        finally
        {
            Console.CursorVisible = true;
            Console.Clear();
        }
    }
}