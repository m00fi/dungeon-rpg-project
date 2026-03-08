namespace dungeonRPG.Items.Currencies;

using dungeonRPG.Entities;

public abstract class Currency : IItem
{
    public abstract char Symbol { get; }
    public abstract string Name { get; }

    public abstract void PickUp(Player player);
}