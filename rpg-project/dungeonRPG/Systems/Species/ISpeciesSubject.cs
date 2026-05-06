namespace dungeonRPG.Systems.Observers;

public interface ISpeciesSubject
{
    void Subscribe(ISpeciesObserver observer);
    void Unsubscribe(ISpeciesObserver observer);
    void NotifyDeath();
}