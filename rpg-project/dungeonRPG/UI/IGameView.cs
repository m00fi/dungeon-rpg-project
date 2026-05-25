using dungeonRPG.Systems.Network.Data;

namespace dungeonRPG.UI;

public interface IGameView
{
    void Render(GameStateDto state, int localPlayerId);
}
