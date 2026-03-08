namespace dungeonRPG.Items.Weapons;

public class Greatbow : Weapon
{
    public override char Symbol => '❵';
    public override string Name => "Greatbow";
    public override int Damage => 30;
    public override bool IsTwoHanded => true;
}