using dungeonRPG.Logging;
using dungeonRPG.Systems.Observers;

namespace dungeonRPG.Entities.Enemies;

public class ArmoredSkeleton : Enemy, ISpeciesObserver
{
    private readonly ISpeciesSubject? _network;

    public ArmoredSkeleton(ISpeciesSubject? network = null, int health = 100, int attack = 15, int armor = 20)
        : base(health, attack, armor)
    {
        _network = network;
        _network?.Subscribe(this);
    }

    public ArmoredSkeleton(int health, int attack, int armor)
        : this(null, health, attack, armor)
    {
    }

    public void OnCompanionDeath()
    {
        Attack = Math.Max(0, Attack - 3);
    }
    
    public override void Die()
    {
        base.Die();
        _network?.Unsubscribe(this);
        _network?.NotifyDeath();
    }

    public override string Name => "Armored Skeleton";
    public override char Symbol => '☠';
}