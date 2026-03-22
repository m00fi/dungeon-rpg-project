namespace dungeonRPG.Dungeon;

using Cells;
using Items.Currencies;
using Items.Weapons;
using dungeonRPG.Items.Others;
using Entities;


public class Room
{
    public const int Height = 20;
    public const int Width = 40;

    private readonly Cell[,] _grid;

    public Room()
    {
        _grid = new Cell[Height, Width];
    }
    public Room(Cell[,] Grid)
    {
        _grid = Grid;
    }

    public Cell GetCell(int x, int y)
    {
        if(x < 0 || x >= Width || y < 0 || y >= Height) return new WallCell(); 
        return _grid[y, x];
    }
    public void SetCell(int x, int y, Cell cell) =>  _grid[y, x] = cell;
}