using dungeonRPG.Logging;
using dungeonRPG.Systems.Observers;

namespace dungeonRPG.Entities.Enemies;

public class Snake : Enemy, ISpeciesObserver
{
    private readonly ISpeciesSubject? _network;

    public Snake(ISpeciesSubject? network = null, int health = 24, int attack = 30, int armor = 3)
        : base(health, attack, armor)
    {
        _network = network;
        _network?.Subscribe(this);
    }

    public Snake(int health, int attack, int armor)
        : this(null, health, attack, armor)
    {
    }

    public void OnCompanionDeath()
    {
        Armor += 2;
    }

    public override void Die()
    {
        base.Die();
        _network?.Unsubscribe(this);
        _network?.NotifyDeath();
    }

    public override string Name => "Snake";
    public override char Symbol => 'ಊ';
}