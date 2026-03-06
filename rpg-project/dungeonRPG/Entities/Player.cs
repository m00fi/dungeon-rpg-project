namespace dungeonRPG.Entities;

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
}