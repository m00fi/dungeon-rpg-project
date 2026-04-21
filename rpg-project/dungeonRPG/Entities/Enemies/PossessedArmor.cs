namespace dungeonRPG.Entities.Enemies;

public class PossessedArmor(int health = 50, int attack = 13, int armor = 30) : Enemy(health, attack, armor)
{
    public override string Name => "Possessed Armor";
    public override char Symbol => '⛨';
}