namespace dungeonRPG.Entities.Enemies;

public class Bat : Enemy
{
    public override string Name => "Bat";
    public override char Symbol => 'Ɓ';
    
    public Bat(int health, int attack, int armor) : base(health, attack, armor)
    {
    }
}