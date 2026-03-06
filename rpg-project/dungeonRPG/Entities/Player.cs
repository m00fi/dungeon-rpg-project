namespace dungeonRPG.Entities;
using Dungeon;
using Dungeon.Cells;

public class Player
{
    public int X;
    public int Y;

    public Player(int startX, int startY)
    {
        X = startX;
        Y = startY;
    }

    public void Move(int dx, int dy)
    {
        X += dx;
        Y += dy;
    }

    public void TryMove(int dx, int dy, Room room)
    {
        int targetX = X + dx;
        int targetY = Y + dy;
        if (targetX < 0 || targetX >= 40 || targetY < 0 || targetY >= 20) return; // Out of bounds

        Cell targetCell = room.GetCell(targetX, targetY);
        
        if (targetCell.Enter(this))
        {
            X = targetX;
            Y = targetY;
        }
    }
}