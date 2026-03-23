using dungeonRPG.Dungeon;

namespace dungeonRPG.Items.Weapons;

using Entities;

public abstract class Weapon : IItem
{
    public abstract char Symbol { get; }
    public abstract string Name { get; }
    public virtual bool IsInventoryItem { get; } = true;
    public abstract int Damage { get; }

    public virtual void PickUp(Player player)
    {
        player.inventory.TryAdd(this);
    }
    public virtual void Use(Player player, Room room)
    {
        player.inventory.Remove(this);

        List<Weapon> unequippedWeapons;
        unequippedWeapons = player.equipment.EquipOneHanded(this);

        foreach (var oldWeapon in unequippedWeapons)
        {
            if (!player.inventory.TryAdd(oldWeapon))
            {
                var currentCell = room.GetCell(player.X, player.Y);
                currentCell.AddItem(oldWeapon);
            }
        }
    }
    
    public virtual string GetDescription()
    {
        return $"({Symbol}) {Name} (Damage: {Damage})";
    }
}