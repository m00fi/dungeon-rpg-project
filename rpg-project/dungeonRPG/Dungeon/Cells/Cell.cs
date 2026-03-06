namespace dungeonRPG.Dungeon.Cells;
using Entities;


public abstract class Cell
{
    public abstract char GetSymbol();
    public abstract bool Enter(Player player);
}