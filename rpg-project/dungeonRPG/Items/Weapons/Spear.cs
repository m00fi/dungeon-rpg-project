namespace dungeonRPG.Items.Weapons;

public class Spear : Weapon
{
    public override char Symbol => '↟';
    public override string Name => "Spear";
    public override int Damage => 25;
    public override bool IsTwoHanded =>  false;
}