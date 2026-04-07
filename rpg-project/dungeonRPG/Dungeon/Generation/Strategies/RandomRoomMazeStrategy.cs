namespace dungeonRPG.Dungeon.Generation.Strategies;

public class RandomRoomMazeStrategy : IDungeonGenerationStrategy
{
    public void Generate(IDungeonBuilder dBuilder)
    {
        dBuilder.BuildFull();

        dBuilder.AddCentralRoom(12, 6);
        dBuilder.AddRooms();
        dBuilder.AddRooms();
        dBuilder.AddRooms();
        dBuilder.AddCorridors();
        dBuilder.AddWeapons();
        dBuilder.AddItems(20);
        dBuilder.AddEnemies(20);
    }
}