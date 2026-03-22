namespace dungeonRPG.Dungeon.Generation.Strategies;

public class BossArenaStrategy : IDungeonGenerationStrategy
{
    public void Generate(IDungeonBuilder dBuilder)
    {
        dBuilder.buildFull();
        dBuilder.AddCentralRoom(30, 12);
        dBuilder.AddItems(20);
        dBuilder.AddWeapons();
        dBuilder.AddStarterRoom();
        dBuilder.AddCorridors();
    }
}