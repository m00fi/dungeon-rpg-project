namespace dungeonRPG.Entities.Enemies;

public class Goblin(int health = 50, int attack = 10, int armor = 10) : Enemy(health, attack, armor)
{
    public override string Name => "Goblin";
    public override char Symbol => 'Ǥ';
}