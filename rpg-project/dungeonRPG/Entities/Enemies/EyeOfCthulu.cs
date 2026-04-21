namespace dungeonRPG.Entities.Enemies;

public class EyeOfCthulu(int health = 50, int attack = 17, int armor = 5) : Enemy(health, attack, armor)
{
    public override string Name => "Eye of Cthulu";
    public override char Symbol => '⦿';
}