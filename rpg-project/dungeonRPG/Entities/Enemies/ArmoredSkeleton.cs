namespace dungeonRPG.Entities.Enemies;

public class ArmoredSkeleton(int health = 100, int attack = 15, int armor = 20) : Enemy(health, attack, armor)
{
    public override string Name => "Armored Skeleton";
    public override char Symbol => '☠';
}