using dungeonRPG.Items;

namespace dungeonRPG.Entities.Modules;

public class Inventory
{
    public int Count => Items.Count;
    public int Capacity { get; private set; } = 20;
    public List<IItem> Items { get; private set; } = new List<IItem>();
    
    public bool TryAdd(IItem item)
    {
        if (Count >= Capacity) return false;
            
        Items.Add(item);
        return true;
    }

    public void Remove(IItem item) => Items.Remove(item);
    
    public bool IsFullInventory() => Count == Capacity;

    public List<string> GetInventory()
    {
        return Items.Select((item, index) => $"{index + 1}. {item.GetDescription()}").ToList();
    }
}