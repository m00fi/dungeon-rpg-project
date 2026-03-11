using dungeonRPG.Dungeon;

namespace dungeonRPG.Items.Others;
using dungeonRPG.Entities;

public abstract class Other : IItem
{
    public abstract char Symbol { get; }
    public abstract string Name { get; }

    public virtual void PickUp(Player player)
    {
        player.inventory.TryAdd(this);
    }
    public virtual void Use(Player player, Room room)
    {
    }
    public virtual string GetDescription()
    {
        return $"({Symbol}) {Name}";
    }
}