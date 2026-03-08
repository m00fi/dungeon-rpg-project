namespace dungeonRPG.Entities.Modules;

public class Money
{
    public int Coins { get; private set; }
    public int Gold { get; private set; }

    public void AddCoins(int amount) => Coins += amount;
    public void AddGold(int amount) => Gold += amount;

    public string GetMoney()
    {
        return $"Coins: {Coins}, Gold: {Gold}";
    }
}