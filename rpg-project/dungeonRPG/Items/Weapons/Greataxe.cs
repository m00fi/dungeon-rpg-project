using dungeonRPG.Items.Weapons.WeaponCategories;

namespace dungeonRPG.Items.Weapons;

public class Greataxe : TwoHandedWeapon, IHeavyWeapon
{
    public override char Symbol => 'ቑ';
    public override string Name => "Greataxe";
    public override int Damage => 35;
    public override void Accept(Combat.IAttackVisitor visitor, Weapon statsSource)
    {
        visitor.Visit(this, statsSource); 
    }
}