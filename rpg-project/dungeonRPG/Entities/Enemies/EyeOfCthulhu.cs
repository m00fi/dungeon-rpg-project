using dungeonRPG.Logging;
using dungeonRPG.Systems.Observers;

namespace dungeonRPG.Entities.Enemies;

public class EyeOfCthulhu : Enemy, ISpeciesObserver
{
    private readonly ISpeciesSubject? _network;

    public EyeOfCthulhu(ISpeciesSubject? network = null, int health = 50, int attack = 17, int armor = 5)
        : base(health, attack, armor)
    {
        _network = network;
        _network?.Subscribe(this);
    }

    public EyeOfCthulhu(int health, int attack, int armor)
        : this(null, health, attack, armor)
    {
    }

    public void OnCompanionDeath()
    {
        Attack += 4;
        Armor += 2;
    }

    public override void Die()
    {
        base.Die();
        _network?.Unsubscribe(this);
        _network?.NotifyDeath();
    }

    public override string Name => "Eye of Cthulhu";
    public override char Symbol => '⦿';
}