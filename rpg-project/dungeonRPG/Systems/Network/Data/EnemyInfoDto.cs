namespace dungeonRPG.Systems.Network.Data;

public class EnemyInfoDto
{
    public char Symbol { get; set; }
    public string Name { get; set; } = "";
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Armor { get; set; }
}
