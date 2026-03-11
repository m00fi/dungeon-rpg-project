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
    
    public virtual int GetItemsCount() => 0;
    public virtual IItem? GetItemAt(int index) => null;
    public virtual IItem? PopItemAt(int index) => null;
}