namespace dungeonRPG.Dungeon.Generation.Strategies;

public interface IDungeonGenerationStrategy
{
    void Generate(IDungeonBuilder dBuilder);
}