using dungeonRPG.Items.Weapons.WeaponCategories;

namespace dungeonRPG.Items.Weapons;

public class Machete : OneHandedWeapon, ILightWeapon
{
    public override char Symbol => '╱';
    public override string Name => "Machete";
    public override int Damage => 36;
    
    public override void Accept(Combat.IAttackVisitor visitor, Weapon statsSource)
    {
        visitor.Visit(this, statsSource); 
    }
}