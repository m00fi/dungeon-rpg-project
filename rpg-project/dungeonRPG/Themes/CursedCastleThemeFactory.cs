using dungeonRPG.Dungeon.Generation.Strategies;
using dungeonRPG.Entities.Enemies;
using dungeonRPG.Items.Artifacts;
using dungeonRPG.Items.Weapons;
using dungeonRPG.Systems.Factions;
using dungeonRPG.Systems.Observers;

namespace dungeonRPG.Themes;

public class CursedCastleThemeFactory : IThemeFactory
{
    private readonly ISpeciesSubject _knightNetwork = new SpeciesNetwork();
    private readonly ISpeciesSubject _skeletonNetwork = new SpeciesNetwork();
    private readonly ISpeciesSubject _armorNetwork = new SpeciesNetwork();
    public string GetMessage()
    {
        return "Metal clanking echoes through the halls, as if something heavy is dragging itself along the ground.";
    }

    public IDungeonGenerationStrategy GetGenerationStrategy()
    {
        return new RandomRoomMazeStrategy();
    }

    public Enemy GetRandomEnemy(Random random)
    {
        int roll = random.Next(0, 3);
        return roll switch {0 => new ArmoredSkeleton(_skeletonNetwork), 1 => new PossessedArmor(_armorNetwork), _ => new EvilKnight(_knightNetwork)};
    }

    public Weapon GetRandomWeapon(Random random)
    {
        int roll = random.Next(0, 3);
        return roll switch {0 => new Spear(), 1 => new Greataxe(), _ => new Machete()};
    }

    public Weapon CreateArtifact()
    {
        return new Greatsword();
    }
}