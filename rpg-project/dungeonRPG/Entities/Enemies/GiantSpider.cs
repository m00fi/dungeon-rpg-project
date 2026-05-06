using dungeonRPG.Logging;
using dungeonRPG.Systems.Observers;

namespace dungeonRPG.Entities.Enemies;

public class GiantSpider : Enemy, ISpeciesObserver
{
    private readonly ISpeciesSubject? _network;

    public GiantSpider(ISpeciesSubject? network = null, int health = 60, int attack = 28, int armor = 9)
        : base(health, attack, armor)
    {
        _network = network;
        _network?.Subscribe(this);
    }

    public GiantSpider(int health, int attack, int armor)
        : this(null, health, attack, armor)
    {
    }

    public void OnCompanionDeath()
    {
        Attack += 4;
    }

    public override void Die()
    {
        base.Die();
        _network?.Unsubscribe(this);
        _network?.NotifyDeath();
    }

    public override string Name => "Giant Spider";
    public override char Symbol => '⎈';
}