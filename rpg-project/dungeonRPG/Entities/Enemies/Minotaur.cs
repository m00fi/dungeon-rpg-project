using dungeonRPG.Logging;
using dungeonRPG.Systems.Observers;

namespace dungeonRPG.Entities.Enemies;

public class Minotaur : Enemy, ISpeciesObserver
{
    private readonly ISpeciesSubject? _network;

    public Minotaur(ISpeciesSubject? network = null, int health = 230, int attack = 23, int armor = 23)
        : base(health, attack, armor)
    {
        _network = network;
        _network?.Subscribe(this);
    }

    public Minotaur(int health, int attack, int armor)
        : this(null, health, attack, armor)
    {
    }

    public void OnCompanionDeath()
    {
        Attack += 5;
    }

    public override void Die()
    {
        base.Die();
        _network?.Unsubscribe(this);
        _network?.NotifyDeath();
    }

    public override string Name => "Minotaur";
    public override char Symbol => 'ϻ';
}