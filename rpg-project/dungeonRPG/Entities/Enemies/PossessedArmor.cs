using dungeonRPG.Logging;
using dungeonRPG.Systems.Observers;

namespace dungeonRPG.Entities.Enemies;

public class PossessedArmor : Enemy, ISpeciesObserver
{
    private readonly ISpeciesSubject? _network;

    public PossessedArmor(ISpeciesSubject? network = null, int health = 50, int attack = 13, int armor = 30)
        : base(health, attack, armor)
    {
        _network = network;
        _network?.Subscribe(this);
    }

    public PossessedArmor(int health, int attack, int armor)
        : this(null, health, attack, armor)
    {
    }

    public void OnCompanionDeath()
    {
        Armor = Math.Max(0, Armor - 4);
    }

    public override void Die()
    {
        base.Die();
        _network?.Unsubscribe(this);
        _network?.NotifyDeath();
    }

    public override string Name => "Possessed Armor";
    public override char Symbol => '⛨';
}