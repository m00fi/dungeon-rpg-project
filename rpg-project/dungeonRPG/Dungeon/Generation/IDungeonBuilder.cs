namespace dungeonRPG.Dungeon.Generation;

public interface IDungeonBuilder
{
    void BuildEmpty();
    void BuildFull();
    void AddCorridors();
    void AddStarterRoom();
    void AddRooms();
    void AddCentralRoom(int width, int height);
    void AddItems(int count);
    void AddWeapons();

    Room GetResult();
    List<string> GetInstructions();
    public List<ConsoleKey> GetKeys();
}