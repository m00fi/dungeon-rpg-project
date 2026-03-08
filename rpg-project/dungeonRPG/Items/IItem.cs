namespace dungeonRPG.Items;

using Entities;

public interface IItem
{
    char Symbol { get; }
    string Name { get; }
    
    void PickUp(Player player);
    string GetDescription();
}