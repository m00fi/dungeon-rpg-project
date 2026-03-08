using dungeonRPG.Items;

namespace dungeonRPG.Entities.Modules;

public class Inventory
{
    public List<IItem> Items { get; private set; } = new List<IItem>();
    
    public void Add(IItem item) => Items.Add(item);
    public void Remove(IItem item) => Items.Remove(item);
}