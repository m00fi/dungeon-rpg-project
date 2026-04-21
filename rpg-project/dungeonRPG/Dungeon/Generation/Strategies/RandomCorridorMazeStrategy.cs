namespace dungeonRPG.Dungeon.Generation.Strategies;

public class RandomCorridorMazeStrategy : IDungeonGenerationStrategy
{
    public void Generate(IDungeonBuilder dBuilder)
    {
        dBuilder.BuildFull();
        dBuilder.AddStarterRoom();
        dBuilder.AddCorridors();
        dBuilder.AddCentralRoom(7, 3);
        dBuilder.AddItems(20);
        dBuilder.AddWeapons();
        dBuilder.AddEnemies(10);
    }
}