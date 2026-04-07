namespace dungeonRPG.Items.Weapons;

public class StrongModifier : WeaponDecorator
{
    public StrongModifier(Weapon weapon) : base(weapon)
    {
    }
    
    public override string Name => $"Strong {_weapon.Name}";
    public override int Damage => _weapon.Damage + 5;
}