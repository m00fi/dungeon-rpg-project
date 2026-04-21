using dungeonRPG.Dungeon.Generation.Strategies;
using dungeonRPG.Entities.Enemies;
using dungeonRPG.Items.Artifacts;
using dungeonRPG.Items.Weapons;

namespace dungeonRPG.Themes;

public class JungleMazeThemeFactory : IThemeFactory
{
    public string GetMessage()
    {
        return
            "You find yourself in a labyrinth made out of leaves and vines that seems to be alive.\n" +
            "The air is thick with the scent of damp earth and decaying plant matter. ";
    }

    public IDungeonGenerationStrategy GetGenerationStrategy()
    {
        return new RandomCorridorMazeStrategy();
    }

    public Enemy GetRandomEnemy(Random random)
    {
        int roll = random.Next(0, 3);
        return roll switch {0 => new Minotaur(), 1 => new Snake(), _ => new GiantSpider()};
    }

    public Weapon GetRandomWeapon(Random random)
    {
        int roll = random.Next(0, 3);
        return roll switch {0 => new Spear(), 1 => new Greataxe(), _ => new Machete()};
    }

    public Weapon CreateArtifact()
    {
        return new VineWhip();
    }
}