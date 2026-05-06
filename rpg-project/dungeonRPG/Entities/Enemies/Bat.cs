using dungeonRPG.Logging;
using dungeonRPG.Systems.Observers;

namespace dungeonRPG.Entities.Enemies;

public class Bat : Enemy, ISpeciesObserver
{
    private readonly ISpeciesSubject? _network;

    public Bat(ISpeciesSubject? network, int health, int attack, int armor)
        : base(health, attack, armor)
    {
        _network = network;
        _network?.Subscribe(this);
    }

    public Bat(int health, int attack, int armor)
        : this(null, health, attack, armor)
    {
    }

    public void OnCompanionDeath()
    {
        Attack = Math.Max(0, Attack - 2);
        Armor = Math.Max(0, Armor - 1);
    }

    public override void Die()
    {
        base.Die();
        _network?.Unsubscribe(this);
        _network?.NotifyDeath();
    }

    public override string Name => "Bat";
    public override char Symbol => 'Ɓ';
}