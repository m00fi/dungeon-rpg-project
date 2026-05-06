using dungeonRPG.Combat;
using dungeonRPG.Dungeon;
using dungeonRPG.Entities.Modules;
using dungeonRPG.Items.Weapons.WeaponCategories;

namespace dungeonRPG.Items.Weapons;

using Entities;

public abstract class Weapon : IItem
{
    public abstract char Symbol { get; }
    public abstract string Name { get; }
    public virtual bool IsInventoryItem { get; } = true;
    public abstract int Damage { get; }
    
    public virtual int StrengthBonus => 0;
    public virtual int DexterityBonus => 0;
    public virtual int HealthBonus => 0;
    public virtual int LuckBonus => 0;
    public virtual int AggressionBonus => 0;
    public virtual int WisdomBonus => 0;
    

    public virtual void PickUp(Player player)
    {
        player.inventory.TryAdd(this);
    }
    
    public virtual void Use(Player player, Room room)
    {
        player.inventory.Remove(this);
        List<Weapon> unequippedWeapons = EquipTo(player.equipment, this);

        foreach (var oldWeapon in unequippedWeapons)
        {
            if (!player.inventory.TryAdd(oldWeapon))
            {
                var currentCell = room.GetCell(player.X, player.Y);
                currentCell.TryAddItem(oldWeapon);
            }
        }
    }
    public abstract List<Weapon> EquipTo(Equipment equipment, Weapon actualWeapon);
    public abstract void Accept(IAttackVisitor visitor, Weapon statsSource);
    
    public virtual string GetDescription()
    {
        return $"({Symbol}) {Name} (Damage: {Damage})";
    }

    public virtual int GetNoiseRange()
    {
        return this switch
        {
            IHeavyWeapon => 14,
            IMagicWeapon => 9,
            ILightWeapon => 5,
            _ => 0
        };
    }
}