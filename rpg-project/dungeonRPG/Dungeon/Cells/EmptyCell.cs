namespace dungeonRPG.Dungeon.Cells;
using Items;

using Entities;
public class EmptyCell : Cell
{
    public List<IItem> Items { get; private set; } = new(); 
    public override char GetSymbol()
    {
        if (Items.Count == 0)
            return ' ';

        return Items.Last().Symbol;
    }

    public override bool Enter(Player player) => true;
    public override void AddItem(IItem item) => Items.Add(item);

    public override IItem? PopItem()
    {
        if (Items.Count == 0) return null;
        var item = Items.Last(); 
        Items.RemoveAt(Items.Count - 1);
        return item;
    }
    public override string? GetTopItemName()
    {
        if(Items.Count == 0) return null;
        return Items.Last().GetDescription();
    }
}