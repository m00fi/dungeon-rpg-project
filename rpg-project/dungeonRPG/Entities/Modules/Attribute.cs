using dungeonRPG.Items.Weapons;

namespace dungeonRPG.Entities.Modules;

public class Attribute
{
    private readonly Equipment _equipment;
    public Attribute(Equipment equipment)
    {
        _equipment = equipment;
    }

    private static readonly int BaseStrength = 5;
    private static readonly int BaseDexterity = 5;
    private static readonly int BaseHealth = 100;
    private static readonly int BaseLuck = 5;
    private static readonly int BaseAggression = 5;
    private static readonly int BaseWisdom = 5;

    public int Strength => BaseStrength + GetBonus(w => w.StrengthBonus);
    public int Dexterity => BaseDexterity + GetBonus(w => w.DexterityBonus);
    public int Health { get; set; } = BaseHealth;

    public int Luck => BaseLuck + GetBonus(w => w.LuckBonus);
    public int Aggression => BaseAggression + GetBonus(w => w.AggressionBonus);
    public int Wisdom => BaseWisdom + GetBonus(w => w.WisdomBonus);

    private int GetBonus(Func<Weapon, int> statSelector)
    {
        int bonus = 0;
        
        if (_equipment.LeftHand != null) 
            bonus += statSelector(_equipment.LeftHand);
        
        if (_equipment.RightHand != null && _equipment.RightHand != _equipment.LeftHand) 
            bonus += statSelector(_equipment.RightHand);
            
        return bonus;
    }

    public List<string> GetAttributes()
    {
        return new List<string>
        {
            $"Health: {Health}",
            $"Strength: {Strength}",
            $"Dexterity: {Dexterity}",
            $"Luck: {Luck}",
            $"Aggression: {Aggression}",
            $"Wisdom: {Wisdom}"
        };
    }
}
