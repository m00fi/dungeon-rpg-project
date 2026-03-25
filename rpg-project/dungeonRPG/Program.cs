using dungeonRPG;
using dungeonRPG.UI;


Console.OutputEncoding = System.Text.Encoding.UTF8;

var dungeonMenu = new DungeonSelectionMenu();
var strategy = dungeonMenu.Display();
var game = new Game(strategy);

game.DisplayMenu();
game.Run();