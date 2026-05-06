using dungeonRPG.Entities.Enemies;

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

    public void MoveEnemies(Player player)
    {
        if(player.IsInCombat) return;
        
        var enemiesToMove = new List<Enemy>();

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (_grid[y, x].Enemy != null) 
                {
                    enemiesToMove.Add(_grid[y, x].Enemy!);
                }
            }
        }

        Random rnd = new Random();

        foreach (var enemy in enemiesToMove)
        {
            if (enemy.Health <= 0) continue;

            int[] dx = { 0, 0, -1, 1 };
            int[] dy = { -1, 1, 0, 0 };
        
            int direction = rnd.Next(4);
        
            int newX = enemy.X + dx[direction];
            int newY = enemy.Y + dy[direction];

            if (newX >= 0 && newX < Width && newY >= 0 && newY < Height)
            {
                var targetCell = _grid[newY, newX];

                if (targetCell.CanHoldItems && targetCell.Enemy == null && !(player.X == newX && player.Y == newY))
                {
                    targetCell.Enemy = enemy;
                    _grid[enemy.Y, enemy.X].Enemy = null;
                    enemy.X = newX;
                    enemy.Y = newY;
                }
            }
        }
    }
}