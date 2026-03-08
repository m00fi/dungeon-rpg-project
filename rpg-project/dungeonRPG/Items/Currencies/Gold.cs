using dungeonRPG.Entities;

namespace dungeonRPG.Items.Currencies;

public class Gold : Currency
{
    public override char Symbol => '▱';
    public override string Name => "Gold";

    public override void PickUp(Player player)
    {
        player.money.AddGold(1);
    }
}