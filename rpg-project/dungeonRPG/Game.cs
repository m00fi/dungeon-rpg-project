using dungeonRPG.Dungeon.Cells;
using dungeonRPG.Dungeon.Generation;
using dungeonRPG.Dungeon.Generation.Strategies;
using dungeonRPG.Entities;
using dungeonRPG.Input;
using dungeonRPG.Input.Handlers;

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
    private readonly IInputHandler _inputHandler;
    
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
        
        _inputHandler = new InventoryInputHandler();
        _inputHandler.SetNext(new GroundInputHandler())
                     .SetNext(new MovementInputHandler())
                     .SetNext(new GlobalActionHandler())
                     .SetNext(new UnboundKeyHandler());
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
        
        _inputHandler = new InventoryInputHandler();
        _inputHandler.SetNext(new GroundInputHandler())
            .SetNext(new MovementInputHandler())
            .SetNext(new GlobalActionHandler())
            .SetNext(new UnboundKeyHandler());
    }

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

                var result = _inputHandler.HandleInput(key, _player, _room);

                if (result.ExitGame)
                    return;
                
                if (result.Message != null)
                {
                    Console.SetCursorPosition(42, 22);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(result.Message.PadRight(40));
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(800);
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