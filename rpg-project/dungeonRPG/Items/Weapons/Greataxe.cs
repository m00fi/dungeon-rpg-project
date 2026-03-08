namespace dungeonRPG.Items.Weapons;

public class Greataxe : Weapon
{
    public override char Symbol => 'ቑ';
    public override string Name => "Greataxe";
    public override int Damage => 35;
    public override bool IsTwoHanded => true;
}