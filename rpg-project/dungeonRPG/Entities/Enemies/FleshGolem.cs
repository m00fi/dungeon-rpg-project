using dungeonRPG.Logging;
using dungeonRPG.Systems.Observers;

namespace dungeonRPG.Entities.Enemies;

public class FleshGolem : Enemy, ISpeciesObserver
{
    private readonly ISpeciesSubject? _network;

    public FleshGolem(ISpeciesSubject? network = null, int health = 230, int attack = 11, int armor = 4)
        : base(health, attack, armor)
    {
        _network = network;
        _network?.Subscribe(this);
    }

    public FleshGolem(int health, int attack, int armor)
        : this(null, health, attack, armor)
    {
    }

    public void OnCompanionDeath()
    {
        Attack += 3;
        Armor += 1;
    }

    public override void Die()
    {
        base.Die();
        _network?.Unsubscribe(this);
        _network?.NotifyDeath();
    }

    public override string Name => "Flesh Golem";
    public override char Symbol => 'Ғ';
}