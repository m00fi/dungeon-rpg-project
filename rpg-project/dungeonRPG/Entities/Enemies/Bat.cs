namespace dungeonRPG.Entities.Enemies;

public class Bat(int health, int attack, int armor) : Enemy(health, attack, armor)
{
    public override string Name => "Bat";
    public override char Symbol => 'Ɓ';
}