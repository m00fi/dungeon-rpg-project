namespace dungeonRPG.Dungeon;

using Cells;
using Entities;


public class Room
{
    public const int Height = 20;
    public const int Width = 40;

    private readonly Cell[,] _grid;

    public Room()
    {
        _grid = new Cell[Height, Width];
        Initialize();
    }

    private void Initialize()
    {
        for (int y = 0; y < Height; y++)
        for (int x = 0; x < Width; x++)
            _grid[y, x] = new EmptyCell();
        
        for (int x = 0; x < Width - 17; x++)
        {
            _grid[5, x] = new WallCell();
            //_grid[Height - 1, x] = new WallCell();
        }
        
        for (int x = 14; x < Width; x++)
        {
            _grid[17, x] = new WallCell();
            //_grid[Height - 1, x] = new WallCell();
        }
        for (int y = 5; y < Height - 4; y++)
        {
            _grid[y, 14] = new WallCell();
            //_grid[y, Width - 1] = new WallCell();
        }
        
        for (int y = 13; y < Height; y++)
        {
            _grid[y, Width - 1] = new WallCell();
            //_grid[y, Width - 1] = new WallCell();
        }
    }

    public Cell GetCell(int x, int y)
    {
        if(x < 0 || x >= Width || y < 0 || y >= Height) return new WallCell(); 
        return _grid[y, x];
    }
    public void SetCell(int x, int y, Cell cell) =>  _grid[y, x] = cell;
}