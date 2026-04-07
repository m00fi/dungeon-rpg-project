using dungeonRPG.Items.Weapons.WeaponCategories;

namespace dungeonRPG.Items.Weapons;

public class Spear : OneHandedWeapon, ILightWeapon
{
    public override char Symbol => 'Î';
    public override string Name => "Spear";
    public override int Damage => 25;
    
    public override void Accept(Combat.IAttackVisitor visitor, Weapon statsSource)
    {
        visitor.Visit(this, statsSource); 
    }
}