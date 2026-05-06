using dungeonRPG.Dungeon.Cells;
using dungeonRPG.Dungeon.Generation;
using dungeonRPG.Dungeon.Generation.Strategies;
using dungeonRPG.Entities;
using dungeonRPG.Input;
using dungeonRPG.Input.Handlers;
using dungeonRPG.Themes;

namespace dungeonRPG;

using Dungeon;
using UI;
public class Game
{
    private readonly Room _room;
    private readonly Display _display;
    private readonly Player _player;
    private readonly List<string> _instructions;
    private readonly List<ConsoleKey> _activeKeys;
    private readonly Menu _menu;
    private readonly IInputHandler _inputHandler;
    private readonly IThemeFactory _theme;
    
    public Game()
    {
        _theme = new CursedCastleThemeFactory();
        
        IDungeonBuilder builder = new DefaultDungeonBuilder(_theme);
        var strategy = _theme.GetGenerationStrategy();
        strategy.Generate(builder);

        _room = builder.GetResult();
        _instructions = builder.GetInstructions();
        _activeKeys = builder.GetKeys();
        
        _menu = new Menu(_instructions);
        
        _display = new Display();
        _player = new Player("m0fi",0, 0);
        
        _inputHandler = new InventoryInputHandler();
        _inputHandler.SetNext(new GroundInputHandler())
                     .SetNext(new MovementInputHandler())
                     .SetNext(new GlobalActionHandler())
                     .SetNext(new UnboundKeyHandler());
    }
    public Game(IThemeFactory theme, string playerName,  string logDirectory)
    {
        IDungeonBuilder builder = new DefaultDungeonBuilder(theme);
        var strategy = theme.GetGenerationStrategy();
        strategy.Generate(builder);
    
        _room = builder.GetResult();
        _instructions = builder.GetInstructions();
        _activeKeys = builder.GetKeys();
    
        _menu = new Menu(_instructions);
        
        _display = new Display();
        _player = new Player(playerName,0, 0);

        _inputHandler = new ExitGameHandler();
        
        _inputHandler.SetNext(new CombatInputHandler())
            .SetNext(new InventoryInputHandler())
            .SetNext(new GroundInputHandler())
            .SetNext(new MovementInputHandler())
            .SetNext(new GlobalActionHandler())
            .SetNext(new UnboundKeyHandler());
        _theme = theme;
    }

    public void DisplayMenu()
    {
        _menu.Display(_theme);
    }
    
    public void Run()
    {   
        Console.CursorVisible = false;
        Console.Clear();
        
        string? currentMessage = null;

        try
        {
            while (true)
            {
                _display.Render(_room, _player, currentMessage, _instructions);
                currentMessage = null;

                var key = Console.ReadKey(intercept: true).Key;
                var result = _inputHandler.HandleInput(key, _player, _room, _activeKeys);
                
                bool isMovementAttempt = key == ConsoleKey.W || key == ConsoleKey.A ||
                                         key == ConsoleKey.S || key == ConsoleKey.D;

                if (result.ExitGame)
                    return;
                
                if (result.Message != null)
                {
                    currentMessage = result.Message;
                }
                
                if(isMovementAttempt)
                    _room.MoveEnemies(_player);
            }
        }
        finally
        {
            Console.CursorVisible = true;
            Console.Clear();
        }
    }
}