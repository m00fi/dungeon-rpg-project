using dungeonRPG.Dungeon.Generation.Strategies;
using dungeonRPG.Entities.Enemies;
using dungeonRPG.Items.Artifacts;
using dungeonRPG.Items.Weapons;
using dungeonRPG.Systems.Factions;
using dungeonRPG.Systems.Observers;

namespace dungeonRPG.Themes;

public class JungleMazeThemeFactory : IThemeFactory
{
    private readonly ISpeciesSubject _minotaurNetwork = new SpeciesNetwork();
    private readonly ISpeciesSubject _snakeNetwork = new SpeciesNetwork();
    private readonly ISpeciesSubject _spiderNetwork = new SpeciesNetwork();
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
        return roll switch {0 => new Minotaur(_minotaurNetwork), 1 => new Snake(_snakeNetwork), _ => new GiantSpider(_spiderNetwork)};
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