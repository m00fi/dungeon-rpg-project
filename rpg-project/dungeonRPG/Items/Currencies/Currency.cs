using dungeonRPG.Dungeon;

namespace dungeonRPG.Items.Currencies;

using dungeonRPG.Entities;

public abstract class Currency : IItem
{
    public abstract int Value { get; }
    public abstract char Symbol { get; }
    public abstract string Name { get; }

    public abstract void PickUp(Player player);

    public virtual void Use(Player player, Room room)
    {
    }
    public virtual string GetDescription()
    {
        return $"({Symbol}) {Name} (+{Value})";
    }
}