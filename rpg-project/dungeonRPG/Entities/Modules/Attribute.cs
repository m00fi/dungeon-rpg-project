namespace dungeonRPG.Entities.Modules;

public class Attribute
{
    public int Strength { get; set; } = 10;
    public int Dexterity { get; set; } = 10;
    public int Health { get; set; } = 100;
    public int Luck { get; set; } = 5;
    public int Aggression { get; set; } = 5;
    public int Wisdom { get; set; } = 5;

    public List<string> GetAttributes()
    {
        return new List<string>
        {
            $"Health: {Health}",
            $"Strength: {Strength}",
            $"Dexterity: {Dexterity}",
            $"Luck: {Luck}",
            $"Aggression: {Aggression}",
            $"Wisdom: {Wisdom}"
        };
    }
}