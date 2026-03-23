namespace dungeonRPG.Dungeon.Generation.Strategies;

public class DefaultTerrainStrategy : IDungeonGenerationStrategy
{
    public void Generate(IDungeonBuilder dBuilder)
    {
        dBuilder.BuildFull();
        dBuilder.AddCentralRoom(12, 6);
        dBuilder.AddRooms();
        dBuilder.AddRooms();
        dBuilder.AddRooms();
        dBuilder.AddCorridors();
    }
}