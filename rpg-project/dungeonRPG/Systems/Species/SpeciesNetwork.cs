using dungeonRPG.Systems.Observers;

namespace dungeonRPG.Systems.Factions;

public class SpeciesNetwork : ISpeciesSubject
{
    private readonly List<ISpeciesObserver> _observers = new List<ISpeciesObserver>();

    public void Subscribe(ISpeciesObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void Unsubscribe(ISpeciesObserver observer)
    {
        if (_observers.Contains(observer))
        {
            _observers.Remove(observer);
        }
    }

    public void NotifyDeath()
    {
        var currentObservers = new List<ISpeciesObserver>(_observers);
        
        foreach (var observer in currentObservers)
        {
            observer.OnCompanionDeath();
        }
    }
}