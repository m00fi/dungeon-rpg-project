using dungeonRPG.Dungeon;

namespace dungeonRPG.Systems.Acoustics;

public interface IAcousticObserver
{
    void OnSoundEmitted(int originX, int originY, int range, string sourceName, Room currentRoom);
}