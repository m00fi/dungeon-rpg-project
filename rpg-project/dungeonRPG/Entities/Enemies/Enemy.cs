namespace dungeonRPG.Entities.Enemies;

public abstract class Enemy
{
    public abstract string Name { get; }
    public abstract char Symbol { get; }
    
    public int Health {get; set;}
    public int Armor {get; protected set;}
    public int Attack {get; protected set;}
    
    private Random _random;
    
    public Enemy(int health, int attack, int armor)
    {
        Health = health;
        Attack = attack;
        Armor = armor;
        _random = new Random();
    }
    public bool IsDead => Health <= 0;
}