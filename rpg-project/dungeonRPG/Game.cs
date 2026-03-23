using dungeonRPG.Dungeon.Cells;
using dungeonRPG.Dungeon.Generation;
using dungeonRPG.Dungeon.Generation.Strategies;
using dungeonRPG.Entities;

namespace dungeonRPG;

using Dungeon;
using UI;
public class Game
{
    private readonly Room _room;
    private readonly Display _display;
    private readonly Player _player;
    private readonly List<string> _instructions;
    private readonly Menu _menu;

    public Game()
    {
        IDungeonGenerationStrategy strategy = new RandomRoomMazeStrategy();
        IDungeonBuilder builder = new DefaultDungeonBuilder();
        strategy.Generate(builder);

        _room = builder.GetResult();
        _instructions = builder.GetInstructions();
        
        _menu = new Menu(_instructions);
        
        _display = new Display();
        _player = new Player("m0fi",0, 0);
    }
    public Game(IDungeonGenerationStrategy strategy)
    {
        IDungeonBuilder builder = new DefaultDungeonBuilder();
        strategy.Generate(builder);
    
        _room = builder.GetResult();
        _instructions = builder.GetInstructions();
    
        _menu = new Menu(_instructions);
        
        _display = new Display();
        _player = new Player("m0fi",0, 0);
    }
    
    // public Game(string heroName)
    // {
    //     IDungeonGenerationStrategy strategy = new BossArenaStrategy();
    //     IDungeonBuilder builder = new DefaultDungeonBuilder();
    //     strategy.Generate(builder);
    //
    //     _room = builder.GetResult();
    //     _instructions = builder.GetInstructions();
    //
    //     _display = new Display();
    //     _player = new Player(heroName,0, 0);
    // }

    public void DisplayMenu()
    {
        _menu.Display();
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
                    
                    case ConsoleKey.E: _player.TryPickUp(_room); break;
                    case ConsoleKey.Q: _player.TryDropItem(_room); break;
                    case ConsoleKey.I: _player.ToggleInventory(); break;
                    case ConsoleKey.T: _player.TryUseItem(_room); break;
                    case ConsoleKey.Y: _player.TryUnequipAll(_room); break;
                    
                    case ConsoleKey.UpArrow: _player.SelectPreviousItem(); break;
                    case ConsoleKey.DownArrow: _player.SelectNextItem(_room); break;
                    
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