using dungeonRPG.Dungeon;
using dungeonRPG.Entities;
using dungeonRPG.Entities.Modules;

namespace dungeonRPG.Items.Weapons;

public abstract class OneHandedWeapon : Weapon
{
    public override List<Weapon> EquipTo(Equipment equipment, Weapon actualWeapon)
    {
        return equipment.EquipOneHanded(actualWeapon);
    }
}