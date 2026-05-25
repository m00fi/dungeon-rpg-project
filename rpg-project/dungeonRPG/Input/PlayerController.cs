using dungeonRPG.Systems.Network.Data;

namespace dungeonRPG.Input;

public class PlayerController
{
    public async Task<PlayerActionDto?> ReadActionAsync()
    {
        return await Task.Run(() =>
        {
            var key = Console.ReadKey(intercept: true).Key;
            return TranslateKey(key);
        });
    }

    private static PlayerActionDto? TranslateKey(ConsoleKey key) => key switch
    {
        ConsoleKey.W         => new PlayerActionDto { Action = PlayerActionType.MoveUp },
        ConsoleKey.S         => new PlayerActionDto { Action = PlayerActionType.MoveDown },
        ConsoleKey.A         => new PlayerActionDto { Action = PlayerActionType.MoveLeft },
        ConsoleKey.D         => new PlayerActionDto { Action = PlayerActionType.MoveRight },
        ConsoleKey.E         => new PlayerActionDto { Action = PlayerActionType.PickUp },
        ConsoleKey.Q         => new PlayerActionDto { Action = PlayerActionType.Drop },
        ConsoleKey.I         => new PlayerActionDto { Action = PlayerActionType.ToggleInventory },
        ConsoleKey.H         => new PlayerActionDto { Action = PlayerActionType.ToggleInstructions },
        ConsoleKey.J         => new PlayerActionDto { Action = PlayerActionType.ToggleLogs },
        ConsoleKey.Y         => new PlayerActionDto { Action = PlayerActionType.UnequipAll },
        ConsoleKey.UpArrow   => new PlayerActionDto { Action = PlayerActionType.SelectPrev },
        ConsoleKey.DownArrow => new PlayerActionDto { Action = PlayerActionType.SelectNext },
        ConsoleKey.D1        => new PlayerActionDto { Action = PlayerActionType.AttackNormal },
        ConsoleKey.D2        => new PlayerActionDto { Action = PlayerActionType.AttackStealth },
        ConsoleKey.D3        => new PlayerActionDto { Action = PlayerActionType.AttackMagic },
        ConsoleKey.Escape    => new PlayerActionDto { Action = PlayerActionType.Exit },
        _                    => null
    };
}
