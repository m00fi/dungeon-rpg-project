namespace dungeonRPG.Dungeon.Generation.Strategies;

public class RandomCorridorMazeStrategy : IDungeonGenerationStrategy
{
    public void Generate(IDungeonBuilder dBuilder)
    {
        dBuilder.BuildFull();
        dBuilder.AddStarterRoom();
        dBuilder.AddCorridors();
    }
}