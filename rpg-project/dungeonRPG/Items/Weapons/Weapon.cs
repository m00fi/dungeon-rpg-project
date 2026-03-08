namespace dungeonRPG.Items.Weapons;

using Entities;

public abstract class Weapon : IItem
{
    public abstract char Symbol { get; }
    public abstract string Name { get; }
    public abstract int Damage { get; }
    public virtual bool IsTwoHanded => false;

    public virtual void PickUp(Player player)
    {
        player.inventory.Add(this);
    }
}