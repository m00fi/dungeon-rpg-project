namespace dungeonRPG.Entities.Enemies;

public class Snake(int health = 24, int attack = 30, int armor = 3) : Enemy(health, attack, armor)
{
    public override string Name => "Snake";
    public override char Symbol => 'ಊ';
}