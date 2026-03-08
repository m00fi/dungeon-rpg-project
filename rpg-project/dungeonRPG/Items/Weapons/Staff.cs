namespace dungeonRPG.Items.Weapons;

public class Staff : Weapon
{
    public override char Symbol => 'ƪ';
    public override string Name => "Staff";
    public override int Damage => 15;
    public override bool IsTwoHanded => false;
}