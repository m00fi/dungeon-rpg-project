using dungeonRPG.Items.Weapons;
using dungeonRPG.Items.Weapons.WeaponCategories;

namespace dungeonRPG.Items.Artifacts;

public class VineWhip : OneHandedWeapon, ILightWeapon
{
    public override char Symbol => '∫';
    public override string Name => "Vine Whip";
    public override int Damage => 63;
    
    public override void Accept(Combat.IAttackVisitor visitor, Weapon statsSource)
    {
        visitor.Visit(this, statsSource); 
    }
}