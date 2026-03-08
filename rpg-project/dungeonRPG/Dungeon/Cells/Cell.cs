namespace dungeonRPG.Dungeon.Cells;
using Entities;
using Items;


public abstract class Cell
{
    public abstract char GetSymbol();
    public abstract bool Enter(Player player);
    public virtual void AddItem(IItem item){}

    public virtual IItem? PopItem()
    {
        return null;
    }
}