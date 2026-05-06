using dungeonRPG.Logging;
using dungeonRPG.Systems.Observers;

namespace dungeonRPG.Entities.Enemies;

public class Goblin : Enemy, ISpeciesObserver
{
    private readonly ISpeciesSubject? _network;

    public Goblin(ISpeciesSubject? network = null, int health = 50, int attack = 10, int armor = 10)
        : base(health, attack, armor)
    {
        _network = network;
        _network?.Subscribe(this);
    }

    public Goblin(int health, int attack, int armor)
        : this(null, health, attack, armor)
    {
    }

    public void OnCompanionDeath()
    {
        Attack += 2;
    }

    public override void Die()
    {
        base.Die();
        _network?.Unsubscribe(this);
        _network?.NotifyDeath();
    }

    public override string Name => "Goblin";
    public override char Symbol => 'Ǥ';
}