namespace dungeonRPG.Entities.Enemies;

public class EvilKnight(int health = 150, int attack = 25, int armor = 25) : Enemy(health, attack, armor)
{
    public override string Name => "Evil Knight";
    public override char Symbol => 'Ҝ';
}