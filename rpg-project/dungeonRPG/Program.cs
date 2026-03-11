using dungeonRPG;
using dungeonRPG.UI;

var menu = new Menu();
var hero = menu.DisplayMenu();
var game = new Game(hero);
game.Run();