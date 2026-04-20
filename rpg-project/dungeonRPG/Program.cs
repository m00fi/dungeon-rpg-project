using dungeonRPG;
using dungeonRPG.Config;
using dungeonRPG.Logging;
using dungeonRPG.UI;


Console.OutputEncoding = System.Text.Encoding.UTF8;

var config = new GameConfig();
config.LoadConfig();

var memLog = new MemoryLogger();
var fileLog = new FileLogger(config.LogDirectory, config.PlayerName);
var compLog = new CompositeLogger(memLog, fileLog);

GameLogger.Initialize(compLog);
GameLogger.Log($"Game started by {config.PlayerName}.");

Console.ReadKey();

var dungeonMenu = new DungeonSelectionMenu();
var strategy = dungeonMenu.Display();
var game = new Game(strategy, config.PlayerName, config.LogDirectory);

game.DisplayMenu();
game.Run();