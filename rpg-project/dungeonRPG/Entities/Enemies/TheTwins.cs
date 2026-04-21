namespace dungeonRPG.Entities.Enemies;

public class TheTwins(int health = 80, int attack = 30, int armor = 3) : Enemy(health, attack, armor)
{
    public override string Name => "The Twins";
    public override char Symbol => 'Ꝏ';
}