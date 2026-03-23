namespace dungeonRPG.Dungeon.Generation.Strategies;

public class FieldWithItemsStrategy : IDungeonGenerationStrategy 
{
    public void Generate(IDungeonBuilder dBuilder)
    {
        dBuilder.BuildEmpty();
        dBuilder.AddItems(80);
        dBuilder.AddWeapons();
        dBuilder.AddWeapons();
        dBuilder.AddWeapons();
        dBuilder.AddWeapons();
        dBuilder.AddWeapons();
        
    }
}