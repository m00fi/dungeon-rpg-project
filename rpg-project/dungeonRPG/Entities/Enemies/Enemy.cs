using dungeonRPG.Dungeon;
using dungeonRPG.Dungeon.Cells;
using dungeonRPG.Logging;
using dungeonRPG.Systems.Acoustics;
using dungeonRPG.Systems.Pathfinding;

namespace dungeonRPG.Entities.Enemies;

public abstract class Enemy : IAcousticObserver
{
    public abstract string Name { get; }
    public abstract char Symbol { get; }
    
    public int X { get; set; }
    public int Y { get; set; }
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
        DungeonAcoustics.Subscribe(this);
    }

    public virtual void Die()
    {
        Health = 0;
        DungeonAcoustics.Unsubscribe(this);
    }
    
    public virtual void OnSoundEmitted(int originX, int originY, int range, string sourceName, Room currentRoom)
    {
        int distance = Pathfinder.CalculateNoiseDistance(X, Y, originX, originY, currentRoom);

        if (distance != -1 && distance <= range)
        {
            GameLogger.Log($"[NOISE] {Name} at ({X},{Y}) heard {sourceName} from {distance} steps away!");
        }
    }
    
    public bool IsDead => Health <= 0;
}