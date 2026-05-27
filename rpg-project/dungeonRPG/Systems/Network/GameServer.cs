using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using dungeonRPG.Dungeon;
using dungeonRPG.Dungeon.Generation;
using dungeonRPG.Entities;
using dungeonRPG.Input;
using dungeonRPG.Input.Handlers;
using dungeonRPG.Logging;
using dungeonRPG.Systems.Network.Data;
using dungeonRPG.Themes;

namespace dungeonRPG.Systems.Network;

public class GameServer
{
    private const int MaxPlayers = 9;

    private readonly TcpListener _listener;
    private readonly Room _room;
    private readonly List<string> _instructions;

    // Game model — every read/write guarded by _modelLock
    private readonly Dictionary<int, Player> _players = new();
    private static readonly object _modelLock = new();

    // Network layer — every read/write guarded by _clientsLock
    private readonly Dictionary<int, StreamWriter> _writers = new();
    private readonly HashSet<int> _usedIds = new();
    private readonly object _clientsLock = new();

    // Input handling — Chain of Responsibility reused from /Input/
    private readonly IInputHandler _inputHandler;
    private readonly List<ConsoleKey> _activeKeys;

    private static readonly Dictionary<PlayerActionType, ConsoleKey> _actionKeyMap = new()
    {
        [PlayerActionType.MoveUp]             = ConsoleKey.W,
        [PlayerActionType.MoveDown]           = ConsoleKey.S,
        [PlayerActionType.MoveLeft]           = ConsoleKey.A,
        [PlayerActionType.MoveRight]          = ConsoleKey.D,
        [PlayerActionType.PickUp]             = ConsoleKey.E,
        [PlayerActionType.Use]                = ConsoleKey.E,
        [PlayerActionType.Drop]               = ConsoleKey.Q,
        [PlayerActionType.UnequipAll]         = ConsoleKey.Y,
        [PlayerActionType.ToggleInventory]    = ConsoleKey.I,
        [PlayerActionType.ToggleLogs]         = ConsoleKey.J,
        [PlayerActionType.ToggleInstructions] = ConsoleKey.H,
        [PlayerActionType.AttackNormal]       = ConsoleKey.D1,
        [PlayerActionType.AttackStealth]      = ConsoleKey.D2,
        [PlayerActionType.AttackMagic]        = ConsoleKey.D3,
        [PlayerActionType.SelectNext]         = ConsoleKey.DownArrow,
        [PlayerActionType.SelectPrev]         = ConsoleKey.UpArrow,
    };

    public GameServer(IThemeFactory theme, int port = 5555)
    {
        IDungeonBuilder builder = new DefaultDungeonBuilder(theme);
        theme.GetGenerationStrategy().Generate(builder);

        _room = builder.GetResult();
        _instructions = builder.GetInstructions();
        _activeKeys = builder.GetKeys();

        _inputHandler = new ExitGameHandler();
        _inputHandler.SetNext(new CombatInputHandler())
                     .SetNext(new InventoryInputHandler())
                     .SetNext(new GroundInputHandler())
                     .SetNext(new MovementInputHandler())
                     .SetNext(new GlobalActionHandler())
                     .SetNext(new UnboundKeyHandler());

        _listener = new TcpListener(IPAddress.Any, port);
        GameLogger.Log($"GameServer initialized on port {port}.");
    }

    public async Task StartAsync()
    {
        using var cts = new CancellationTokenSource();

        try
        {
            _listener.Start();
        }
        catch
        {
            Console.WriteLine("Failed to start the server. (Address already in use).");
            Console.CursorVisible = true;
            return;
        }
        Console.CursorVisible = false;
        Console.WriteLine($"Server listening. Waiting for up to {MaxPlayers} players... [ESC to stop]");
        GameLogger.Log("Server started.");

        _ = Task.Run(() =>
        {
            while (!cts.IsCancellationRequested)
            {
                if (Console.KeyAvailable && Console.ReadKey(intercept: true).Key == ConsoleKey.Escape)
                {
                    cts.Cancel();
                    return;
                }
                Thread.Sleep(50);
            }
        });

        try
        {
            while (!cts.IsCancellationRequested)
            {
                var tcpClient = await _listener.AcceptTcpClientAsync(cts.Token);
                _ = Task.Run(() => HandleNewClientAsync(tcpClient));
            }
        }
        catch (OperationCanceledException) { }
        finally
        {
            _listener.Stop();
            ShutdownAllClients();
            Console.Clear();
            Console.CursorVisible = true;
            GameLogger.Log("Server stopped.");
        }
    }

    private void ShutdownAllClients()
    {
        List<StreamWriter> writers;
        lock (_clientsLock)
        {
            writers = _writers.Values.ToList();
            _writers.Clear();
            _usedIds.Clear();
        }

        foreach (var w in writers)
            try { w.Dispose(); } catch { }

        lock (_modelLock)
        {
            foreach (var p in _players.Values)
                p.Disconnect();
            _players.Clear();
        }
    }

    // -------------------------------------------------------------------------
    // Connection lifecycle
    // -------------------------------------------------------------------------

    private async Task HandleNewClientAsync(TcpClient tcpClient)
    {
        int clientId;
        lock (_clientsLock)
        {
            clientId = AllocateId();
            if (clientId == -1)
            {
                tcpClient.Close();
                return;
            }
            _usedIds.Add(clientId);
        }

        var stream = tcpClient.GetStream();
        var writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true)
        {
            AutoFlush = true,
            NewLine = "\n"
        };
        var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);

        string playerName;
        lock (_modelLock)
        {
            var (spawnX, spawnY) = FindSpawnPoint();
            var player = new Player($"Player{clientId}", spawnX, spawnY);
            player.Symbol = (char)('0' + clientId);
            _players[clientId] = player;
            playerName = player.Name;
        }

        lock (_clientsLock)
        {
            _writers[clientId] = writer;
        }

        // Tell this client its assigned player slot before the first broadcast
        var welcome = new WelcomeDto { PlayerId = clientId };
        await writer.WriteLineAsync(JsonSerializer.Serialize(welcome));

        GameLogger.Log($"{playerName} connected.");
        await BroadcastAsync();

        try
        {
            while (true)
            {
                string? line = await reader.ReadLineAsync();
                if (line == null) break;

                PlayerActionDto? action;
                try { action = JsonSerializer.Deserialize<PlayerActionDto>(line); }
                catch { continue; }

                if (action == null) continue;
                if (action.Action == PlayerActionType.Exit) break;

                string? message;
                lock (_modelLock)
                {
                    message = ApplyAction(clientId, action);
                }

                // if (message != null)
                //     GameLogger.Log(message);

                await BroadcastAsync(message, clientId);
            }
        }
        catch (Exception ex) when (ex is IOException or ObjectDisposedException)
        {
            // Normal disconnect path
        }
        finally
        {
            await DisconnectClientAsync(clientId, writer, reader, tcpClient);
        }
    }

    private async Task DisconnectClientAsync(int clientId, StreamWriter writer, StreamReader reader, TcpClient tcpClient)
    {
        string disconnectedName = $"Player{clientId}";
        lock (_modelLock)
        {
            if (_players.TryGetValue(clientId, out var p))
            {
                disconnectedName = p.Name;
                p.Disconnect();
            }
            _players.Remove(clientId);
        }
        lock (_clientsLock)
        {
            _writers.Remove(clientId);
            _usedIds.Remove(clientId);
        }

        try { writer.Dispose(); } catch { /* ignore */ }
        try { reader.Dispose(); } catch { /* ignore */ }
        try { tcpClient.Close(); } catch { /* ignore */ }

        GameLogger.Log($"{disconnectedName} disconnected.");
        await BroadcastAsync();
    }

    // -------------------------------------------------------------------------
    // State broadcast
    // -------------------------------------------------------------------------

    private async Task BroadcastAsync(string? actionMessage = null, int actingClientId = -1)
    {
        GameStateDto baseState;
        lock (_modelLock) { baseState = BuildGameStateDto(null); }
        string jsonForOthers = JsonSerializer.Serialize(baseState);

        string jsonForActing = jsonForOthers;
        if (actingClientId != -1 && actionMessage != null)
        {
            baseState.CurrentMessage = actionMessage;
            jsonForActing = JsonSerializer.Serialize(baseState);
        }

        List<(int id, StreamWriter writer)> targets;
        lock (_clientsLock) { targets = _writers.Select(kv => (kv.Key, kv.Value)).ToList(); }

        await Task.WhenAll(targets.Select(async t =>
        {
            string json = t.id == actingClientId ? jsonForActing : jsonForOthers;
            try { await t.writer.WriteLineAsync(json); }
            catch { /* dead connection — will clean up in its own task */ }
        }));
    }

    // -------------------------------------------------------------------------
    // Action processing  (must be called under _modelLock)
    // -------------------------------------------------------------------------

    private string? ApplyAction(int clientId, PlayerActionDto action)
    {
        if (!_players.TryGetValue(clientId, out var player)) return null;
        if (!_actionKeyMap.TryGetValue(action.Action, out var key)) return null;

        var result = _inputHandler.HandleInput(key, player, _room, _activeKeys);

        bool isMovement = action.Action is PlayerActionType.MoveUp or PlayerActionType.MoveDown
                                        or PlayerActionType.MoveLeft or PlayerActionType.MoveRight;
        if (isMovement)
        {
            _room.MoveEnemies(player);
            player.Symbol = (char)('0' + clientId);
        }

        return result.Message;
    }

    // -------------------------------------------------------------------------
    // State snapshot  (must be called under _modelLock)
    // -------------------------------------------------------------------------

    private GameStateDto BuildGameStateDto(string? currentMessage)
    {
        var mapRows = new string[Room.Height];
        for (int y = 0; y < Room.Height; y++)
        {
            var sb = new StringBuilder(Room.Width);
            for (int x = 0; x < Room.Width; x++)
                sb.Append(_room.GetCell(x, y).GetSymbol());
            mapRows[y] = sb.ToString();
        }

        var players = new Dictionary<int, PlayerInfoDto>();
        foreach (var (id, p) in _players)
            players[id] = BuildPlayerInfoDto(id, p);

        return new GameStateDto
        {
            MapRows = mapRows,
            Players = players,
            CurrentMessage = currentMessage,
            RecentLogs = GameLogger.GetRecentLogs(10),
            AllLogs = GameLogger.GetAllLogs(),
            Instructions = _instructions
        };
    }

    private PlayerInfoDto BuildPlayerInfoDto(int id, Player player)
    {
        var cell = _room.GetCell(player.X, player.Y);
        return new PlayerInfoDto
        {
            Id = id,
            Symbol = player.Symbol,
            Name = player.Name,
            X = player.X,
            Y = player.Y,
            StatLines = player.stats.GetAttributes(),
            MoneyLine = player.money.GetMoney(),
            LeftHandDescription = player.equipment.GetLeftHandName(),
            RightHandDescription = player.equipment.GetRightHandName(),
            InventoryItems = player.inventory.GetInventory(),
            InventoryCapacity = player.inventory.Capacity,
            GroundItems = cell.GetItemDescriptions(),
            IsInventoryActive = player.IsInventoryActive,
            SelectedInventoryIndex = player.SelectedInventoryIndex,
            SelectedItemIndex = player.SelectedItemIndex,
            IsInstructionsOpen = player.IsInstructionsOpen,
            IsLogsOpen = player.IsLogsOpen,
            IsInCombat = player.IsInCombat,
            ActiveEnemy = player.ActiveEnemy != null ? new EnemyInfoDto
            {
                Symbol = player.ActiveEnemy.Symbol,
                Name = player.ActiveEnemy.Name,
                Health = player.ActiveEnemy.Health,
                Attack = player.ActiveEnemy.Attack,
                Armor = player.ActiveEnemy.Armor
            } : null
        };
    }

    // -------------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------------

    private int AllocateId()
    {
        for (int i = 1; i <= MaxPlayers; i++)
            if (!_usedIds.Contains(i)) return i;
        return -1;
    }

    private (int x, int y) FindSpawnPoint()
    {
        // Called under _modelLock
        var occupied = _players.Values.Select(p => (p.X, p.Y)).ToHashSet();
        for (int y = 0; y < Room.Height; y++)
            for (int x = 0; x < Room.Width; x++)
            {
                var cell = _room.GetCell(x, y);
                if (cell.CanHoldItems && cell.Enemy == null && !occupied.Contains((x, y)))
                    return (x, y);
            }
        return (0, 0);
    }
}
