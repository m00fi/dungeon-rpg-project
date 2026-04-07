namespace dungeonRPG.Entities.Enemies;

public class EvilKnight : Enemy
{
    public override string Name => "Evil Knight";
    public override char Symbol => 'Ҝ';
    
    public EvilKnight(int health, int attack, int armor) : base(health, attack, armor)
    {
    }
}