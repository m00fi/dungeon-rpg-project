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
        _player = new Player("m0fi",0, 0);
    }

    public void Menu()
    {
        Console.CursorVisible = false;
        Console.Clear();
        string logo = """
                      ________                                            
                      \______ \  __ __  ____    ____   ____  ____    ____  
                       |    |  \|  |  \/    \  / ___\_/ __ \/  _ \ /    \ 
                       |    `   \  |  /   |  \/ /_/  >  ___(  <_> )   |  \
                      /_______  /____/|___|  /\___  / \___  >____/|___|  /
                              \/           \//_____/      \/           \/ 
                      """;

        Console.WriteLine(logo);
        Console.WriteLine("Controls:");
        Console.WriteLine("[WASD] - Move");
        Console.WriteLine("[E] - Pick up item");
        Console.WriteLine("[Q] - Drop item");
        Console.WriteLine("[I] - Manage inventory");
        Console.WriteLine("[H] - Help");
        Console.WriteLine("[ESC] - Exit game");
        Console.WriteLine();
        Console.WriteLine("Press any key to start...");
        Console.ReadKey(intercept: true);
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
                    case ConsoleKey.E: _player.TryPickUp(_room); break;
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