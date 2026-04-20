using dungeonRPG;
using dungeonRPG.Config;
using dungeonRPG.UI;


Console.OutputEncoding = System.Text.Encoding.UTF8;

var config = new GameConfig();
config.LoadConfig();

var dungeonMenu = new DungeonSelectionMenu();
var strategy = dungeonMenu.Display();
var game = new Game(strategy, config.PlayerName, config.LogDirectory);

game.DisplayMenu();
game.Run();