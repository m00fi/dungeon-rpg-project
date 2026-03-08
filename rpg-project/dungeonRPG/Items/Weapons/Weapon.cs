namespace dungeonRPG.Items.Weapons;

using Entities;

public abstract class Weapon : IItem
{
    public abstract char Symbol { get; }
    public abstract string Name { get; }
    public abstract int Damage { get; }
    public abstract bool IsTwoHanded { get; }

    public virtual void PickUp(Player player)
    {
        // player.EquipWeapon(this);
    }
}