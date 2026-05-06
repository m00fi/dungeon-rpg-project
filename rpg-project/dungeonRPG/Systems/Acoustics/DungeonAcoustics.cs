using dungeonRPG.Dungeon;

namespace dungeonRPG.Systems.Acoustics;

public static class DungeonAcoustics
{
    private static readonly List<IAcousticObserver> _listeners = new();

    public static void Subscribe(IAcousticObserver listener)
    {
        if (!_listeners.Contains(listener)) _listeners.Add(listener);
    }

    public static void Unsubscribe(IAcousticObserver listener)
    {
        _listeners.Remove(listener);
    }

    public static void EmitSound(int x, int y, int range, string sourceName, Room room)
    {
        var currentListeners = new List<IAcousticObserver>(_listeners);
        foreach (var listener in currentListeners)
        {
            listener.OnSoundEmitted(x, y, range, sourceName, room);
        }
    }
}