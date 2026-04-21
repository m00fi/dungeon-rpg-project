namespace dungeonRPG.Entities.Enemies;

public class Minotaur(int health = 230, int attack = 23, int armor = 23) : Enemy(health, attack, armor)
{
    public override string Name => "Minotaur";
    public override char Symbol => 'ϻ';
}