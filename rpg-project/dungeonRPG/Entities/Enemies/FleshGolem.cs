namespace dungeonRPG.Entities.Enemies;

public class FleshGolem(int health = 230, int attack = 11, int armor = 4) : Enemy(health, attack, armor)
{
    public override string Name => "Flesh Golem";
    public override char Symbol => 'Ғ';
}