namespace dungeonRPG.Items.Others;
using dungeonRPG.Entities;

public abstract class Other : IItem
{
    public abstract char Symbol { get; }
    public abstract string Name { get; }

    public virtual void PickUp(Player player)
    {
        player.inventory.Add(this);
    }
}