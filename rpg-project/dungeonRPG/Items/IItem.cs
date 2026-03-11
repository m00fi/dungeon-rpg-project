using dungeonRPG.Dungeon;

namespace dungeonRPG.Items;

using Entities;

public interface IItem
{
    char Symbol { get; }
    string Name { get; }
    bool IsInventoryItem { get; }
    
    void PickUp(Player player);
    void Use(Player player, Room room);
    string GetDescription();
}