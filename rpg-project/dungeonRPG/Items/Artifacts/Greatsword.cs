using dungeonRPG.Items.Weapons;
using dungeonRPG.Items.Weapons.WeaponCategories;

namespace dungeonRPG.Items.Artifacts;

public class Greatsword : TwoHandedWeapon, IHeavyWeapon
{
    public override char Symbol => '╀';
    public override string Name => "Greatsword";
    public override int Damage => 90;
    
    public override void Accept(Combat.IAttackVisitor visitor, Weapon statsSource)
    {
        visitor.Visit(this, statsSource); 
    }
}