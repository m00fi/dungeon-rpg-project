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
        _player = new Player(0, 0);
    }

    public void Run()
    {
        Console.CursorVisible = false;
        Console.Clear();

        try
        {
            while (true)
            {
                _display.Render(_room, _player);

                var key = Console.ReadKey(intercept: true).Key;

                switch (key)
                {
                    case ConsoleKey.W: _player.TryMove(0, -1, _room); break;
                    case ConsoleKey.S: _player.TryMove(0, 1, _room); break;
                    case ConsoleKey.A: _player.TryMove(-1, 0, _room); break;
                    case ConsoleKey.D: _player.TryMove(1, 0, _room); break;
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