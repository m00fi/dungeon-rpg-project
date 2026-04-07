using dungeonRPG.Items.Weapons;
using dungeonRPG.Items.Weapons.WeaponCategories;

namespace dungeonRPG.Combat;

public interface IAttackVisitor
{
    string CombatMessage { get; }
    void Visit(IHeavyWeapon categoryToken, Weapon statsSource);
    void Visit(ILightWeapon categoryToken, Weapon statsSource);
    void Visit(IMagicWeapon categoryToken, Weapon statsSource);
    
    void VisitUnarmed();
}