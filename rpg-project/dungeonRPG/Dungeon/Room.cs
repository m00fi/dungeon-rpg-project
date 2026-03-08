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
        Initialize();
    }

    private void Initialize()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                _grid[y, x] = new EmptyCell();
            }
        }

        for (int y = 0; y < Height; y++)
        {
            if (y != 15 && y != 16) 
            {
                _grid[y, 15] = new WallCell();
                _grid[y, 16] = new WallCell();
            }
        }
        
        for (int x = 15; x < Width; x++)
        {
            if (x < 28 || x > 30)
            {
                _grid[9, x] = new WallCell();
            }
        }
        
        for (int x = 0; x < 8; x++)
        {
            if (x != 3 && x != 4)
            {
                _grid[12, x] = new WallCell();
            }
        }

        for (int y = 12; y < Height; y++)
        {
            _grid[y, 7] = new WallCell();
            _grid[y, 8] = new WallCell();
        }
        
        _grid[2,2].AddItem(new Staff());
        _grid[2, 2].AddItem(new Greataxe());
        _grid[9,10].AddItem(new Staff());
        _grid[8, 13].AddItem(new Spear());

        _grid[3, 4].AddItem(new Gold());
        _grid[18, 33].AddItem(new Coin());
        _grid[17, 33].AddItem(new Coin());
        _grid[15, 31].AddItem(new Gold());
        _grid[14, 32].AddItem(new Gold());
        
        _grid[10, 22].AddItem(new Quiver());
        _grid[4, 25].AddItem(new Fireball());
        _grid[5, 23].AddItem(new Greatbow());
    }

    public Cell GetCell(int x, int y)
    {
        if(x < 0 || x >= Width || y < 0 || y >= Height) return new WallCell(); 
        return _grid[y, x];
    }
    public void SetCell(int x, int y, Cell cell) =>  _grid[y, x] = cell;
}