namespace dungeonRPG.Items.Weapons;

public class HeavyModifier : WeaponDecorator
{
    public HeavyModifier(Weapon weapon) : base(weapon)
    {
    }
    
    public override string Name => $"Heavy {_weapon.Name}";
    public override int Damage => _weapon.Damage + 20;
    public override int DexterityBonus => _weapon.DexterityBonus - 10;
}