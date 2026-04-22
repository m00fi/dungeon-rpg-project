namespace dungeonRPG.Entities.Enemies;

public class EyeOfCthulhu(int health = 50, int attack = 17, int armor = 5) : Enemy(health, attack, armor)
{
    public override string Name => "Eye of Cthulhu";
    public override char Symbol => '⦿';
}