namespace dungeonRPG;

using Dungeon;
using UI;
public class Game
{
    private readonly Room _room;
    private readonly Display _display;

    public Game()
    {
        _room = new Room();
        _display = new Display();
    }

    public void Run()
    {
        Console.CursorVisible = false;
        Console.Clear();

        _display.RenderDungeon(_room);

        Console.ReadKey();
    }
}