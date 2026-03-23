using dungeonRPG;
using dungeonRPG.UI;

var dungeonMenu = new DungeonSelectionMenu();
var strategy = dungeonMenu.Display();

var game = new Game(strategy);
game.DisplayMenu();
game.Run();