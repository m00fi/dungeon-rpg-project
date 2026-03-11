using System.Runtime.InteropServices;
using dungeonRPG.Entities;

namespace dungeonRPG.Items.Currencies;

public class Coin : Currency
{
    public override int Value { get; }
    public override char Symbol => '©';
    public override string Name => "Coin";

    public Coin(int value)
    {
        Value = value;
    }

    public override void PickUp(Player player)
    {
        player.money.AddCoins(Value);
    }
}