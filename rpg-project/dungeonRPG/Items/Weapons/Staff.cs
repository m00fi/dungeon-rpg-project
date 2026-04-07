using dungeonRPG.Items.Weapons.WeaponCategories;

namespace dungeonRPG.Items.Weapons;

public class Staff : OneHandedWeapon, IMagicWeapon
{
    public override char Symbol => 'ƪ';
    public override string Name => "Staff";
    public override int Damage => 15;
    
    public override void Accept(Combat.IAttackVisitor visitor, Weapon statsSource)
    {
        visitor.Visit(this, statsSource); 
    }
}