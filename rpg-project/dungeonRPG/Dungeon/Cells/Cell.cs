namespace dungeonRPG.Dungeon.Cells;
using Entities;
using Items;


public abstract class Cell
{
    public abstract char GetSymbol();
    public abstract bool Enter(Player player);

    public virtual void AddItem(IItem item)
    {
    }

    public virtual IItem? PopItem() => null;
    public virtual string? GetTopItemName() => null;
    public virtual List<string> GetItemDescriptions() => new List<string>();
}