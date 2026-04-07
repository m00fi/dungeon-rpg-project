using System.Runtime.InteropServices.ComTypes;

namespace dungeonRPG.Items.Weapons;

public class UnluckyModifier : WeaponDecorator
{
    public UnluckyModifier(Weapon weapon) : base(weapon)
    {
    }
    
    public override string Name => $"Unlucky {_weapon.Name}";
    public override int LuckBonus => _weapon.LuckBonus - 10;
}