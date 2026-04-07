namespace dungeonRPG.Entities.Enemies;

public class Goblin : Enemy
{
    public override string Name => "Goblin";
    public override char Symbol => 'g';
    
    public Goblin(int health, int attack, int armor) : base(health, attack, armor)
    {
    }
}