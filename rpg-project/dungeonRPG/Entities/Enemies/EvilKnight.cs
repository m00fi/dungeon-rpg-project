using dungeonRPG.Logging;
using dungeonRPG.Systems.Observers;

namespace dungeonRPG.Entities.Enemies;

public class EvilKnight : Enemy, ISpeciesObserver
{
    private readonly ISpeciesSubject? _network;

    public EvilKnight(ISpeciesSubject? network = null, int health = 150, int attack = 25, int armor = 25)
        : base(health, attack, armor)
    {
        _network = network;
        _network?.Subscribe(this);
    }

    public EvilKnight(int health, int attack, int armor)
        : this(null, health, attack, armor)
    {
    }

    public void OnCompanionDeath()
    {
        Attack += 5;
        Armor += 5;
    }
    public override void Die()
    {
        base.Die();
        _network?.Unsubscribe(this);
        _network?.NotifyDeath();
    }
    
    public override string Name => "Evil Knight";
    public override char Symbol => 'Ҝ';
}