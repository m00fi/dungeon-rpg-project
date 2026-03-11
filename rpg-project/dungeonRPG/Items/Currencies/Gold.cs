using dungeonRPG.Entities;

namespace dungeonRPG.Items.Currencies;

public class Gold : Currency
{
    public override int Value { get; }
    public override char Symbol => '▱';
    public override string Name => "Gold";

    public Gold(int value)
    {
        Value = value;
    }

    public override void PickUp(Player player)
    {
        player.money.AddGold(Value);
    }
}