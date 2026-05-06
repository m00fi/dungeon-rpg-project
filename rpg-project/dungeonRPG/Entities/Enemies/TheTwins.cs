using dungeonRPG.Logging;
using dungeonRPG.Systems.Observers;

namespace dungeonRPG.Entities.Enemies;

public class TheTwins : Enemy, ISpeciesObserver
{
    private readonly ISpeciesSubject? _network;

    public TheTwins(ISpeciesSubject? network = null, int health = 80, int attack = 30, int armor = 3)
        : base(health, attack, armor)
    {
        _network = network;
        _network?.Subscribe(this);
    }

    public TheTwins(int health, int attack, int armor)
        : this(null, health, attack, armor)
    {
    }

    public void OnCompanionDeath()
    {
        Attack += 6;
    }

    public override void Die()
    {
        base.Die();
        _network?.Unsubscribe(this);
        _network?.NotifyDeath();
    }

    public override string Name => "The Twins";
    public override char Symbol => 'Ꝏ';
}