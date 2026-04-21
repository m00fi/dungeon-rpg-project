using dungeonRPG.Items.Weapons;
using dungeonRPG.Items.Weapons.WeaponCategories;

namespace dungeonRPG.Items.Artifacts;

public class OpticStaff : TwoHandedWeapon, IMagicWeapon
{
    public override char Symbol => '⚲';
    public override string Name => "Optic Staff";
    public override int Damage => 80;
    
    public override void Accept(Combat.IAttackVisitor visitor, Weapon statsSource)
    {
        visitor.Visit(this, statsSource); 
    }
}