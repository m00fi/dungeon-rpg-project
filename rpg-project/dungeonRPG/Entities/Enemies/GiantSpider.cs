namespace dungeonRPG.Entities.Enemies;

public class GiantSpider(int health = 60, int attack = 28, int armor = 9) : Enemy(health, attack, armor)
{
    public override string Name => "Giant Spider";
    public override char Symbol => '⎈';
}