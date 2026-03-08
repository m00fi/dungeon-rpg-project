using dungeonRPG.Entities;

namespace dungeonRPG.Items.Currencies;

public class Coin : Currency
{
    public override char Symbol => '©';
    public override string Name => "Coin";

    public override void PickUp(Player player)
    {
        player.money.AddCoins(1);
    }
}