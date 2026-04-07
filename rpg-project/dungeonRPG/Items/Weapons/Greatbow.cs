using dungeonRPG.Items.Weapons.WeaponCategories;

namespace dungeonRPG.Items.Weapons;

public class Greatbow : TwoHandedWeapon, IHeavyWeapon
{
    public override char Symbol => '❵';
    public override string Name => "Greatbow";
    public override int Damage => 30;
    
    public override void Accept(Combat.IAttackVisitor visitor, Weapon statsSource)
    {
        visitor.Visit(this, statsSource); 
    }
}