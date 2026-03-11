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
        for (int x = 0; x < Width; x++)
        {
            if (x != 4 && x != 20)
            {
                _grid[8, x] = new WallCell();
            }
        }
        for (int y = 0; y < 8; y++)
        {
            if (y != 3)
            {
                _grid[y, 8] = new WallCell();
            }
        }
        for (int y = 9; y < Height; y++)
        {
            if (y != 14 && y != 15)
            {
                _grid[y, 25] = new WallCell();
            }
        }
        
        _grid[3, 3].AddItem(new Staff());
        _grid[3, 3].AddItem(new Coin(2));
        _grid[3, 3].AddItem(new HealthPotion());

        _grid[1, 6].AddItem(new Spear());
        _grid[2, 2].AddItem(new Greataxe());
        _grid[5, 5].AddItem(new Staff());
        _grid[6, 2].AddItem(new Greatbow());
        _grid[4, 6].AddItem(new Quiver());

        _grid[2, 15].AddItem(new Greatbow());
        _grid[2, 15].AddItem(new Quiver());
        _grid[1, 38].AddItem(new Gold(4));
        _grid[1, 38].AddItem(new Gold(5));
        _grid[1, 38].AddItem(new Coin(3));

        _grid[2, 12].AddItem(new Spear());
        _grid[5, 18].AddItem(new HealthPotion());
        _grid[6, 22].AddItem(new Fireball());
        _grid[3, 28].AddItem(new Greataxe());
        _grid[7, 34].AddItem(new Staff());

        _grid[18, 2].AddItem(new Staff());
        _grid[18, 2].AddItem(new Fireball());
        _grid[18, 2].AddItem(new HealthPotion());

        _grid[11, 10].AddItem(new Coin(2));
        _grid[13, 14].AddItem(new Coin(4));
        _grid[15, 18].AddItem(new Coin(5));
        _grid[17, 22].AddItem(new Coin(2));

        _grid[10, 2].AddItem(new Greataxe());
        _grid[12, 6].AddItem(new Greatbow());
        _grid[16, 4].AddItem(new Quiver());
        _grid[17, 12].AddItem(new Spear());
        _grid[12, 18].AddItem(new Fireball());
        _grid[14, 22].AddItem(new Staff());

        _grid[14, 32].AddItem(new Greataxe());
        _grid[14, 32].AddItem(new Gold(3));
        _grid[14, 32].AddItem(new Gold(4));
        _grid[14, 32].AddItem(new Gold(1));
        _grid[14, 32].AddItem(new Coin(2));
        _grid[14, 32].AddItem(new HealthPotion());
        _grid[14, 32].AddItem(new Quiver());

        _grid[10, 28].AddItem(new Greatbow());
        _grid[12, 36].AddItem(new Spear());
        _grid[17, 28].AddItem(new Staff());
        _grid[18, 38].AddItem(new Greataxe());
    }

    public Cell GetCell(int x, int y)
    {
        if(x < 0 || x >= Width || y < 0 || y >= Height) return new WallCell(); 
        return _grid[y, x];
    }
    public void SetCell(int x, int y, Cell cell) =>  _grid[y, x] = cell;
}