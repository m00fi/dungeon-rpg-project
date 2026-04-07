using dungeonRPG.Dungeon;
using dungeonRPG.Entities;
using dungeonRPG.Entities.Modules;

namespace dungeonRPG.Items.Weapons;

public abstract class WeaponDecorator : Weapon
{
    protected Weapon _weapon;

    public WeaponDecorator(Weapon weapon)
    {
        _weapon = weapon;
    }

    public override string Name => _weapon.Name;
    public override int Damage => _weapon.Damage;
    public override char Symbol => _weapon.Symbol;
    
    public override List<Weapon> EquipTo(Equipment equipment, Weapon actualWeapon)
    {
        return _weapon.EquipTo(equipment, actualWeapon);
    }

    public override void Accept(Combat.IAttackVisitor visitor, Weapon statsSource)
    {
        _weapon.Accept(visitor, statsSource);
    }
}