using dungeonRPG.Dungeon.Generation.Strategies;
using dungeonRPG.Entities.Enemies;
using dungeonRPG.Items.Artifacts;
using dungeonRPG.Items.Weapons;

namespace dungeonRPG.Themes;

public class CrimsonThemeFactory : IThemeFactory
{
    public string GetMessage()
    {
        return "You feel an ominous presence watching you. This is going to be a terrible night.";
    }

    public IDungeonGenerationStrategy GetGenerationStrategy()
    {
        return new BossArenaStrategy();
    }

    public Enemy GetRandomEnemy(Random random)
    {
        int roll = random.Next(0, 3);
        return roll switch {0 => new EyeOfCthulu(), 1 => new TheTwins(), _ => new FleshGolem()};
    }

    public Weapon GetRandomWeapon(Random random)
    {
        int roll = random.Next(0, 3);
        return roll switch {0 => new Greatbow(), 1 => new Spear(), _ => new Staff()};
    }

    public Weapon CreateArtifact()
    {
        return new OpticStaff();
    }
}