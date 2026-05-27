using dungeonRPG;
using dungeonRPG.Config;
using dungeonRPG.Logging;
using dungeonRPG.Systems.Network;
using dungeonRPG.UI;

Console.OutputEncoding = System.Text.Encoding.UTF8;

if (args.Length >= 1)
{
    switch (args[0])
    {
        case "--server":
        {
            int port = args.Length >= 2 && int.TryParse(args[1], out int p) ? p : 5555;
            InitializeLogging();
            var theme = new DungeonSelectionMenu().Display();
            Console.Clear();
            await new GameServer(theme, port).StartAsync();
            break;
        }
        case "--client":
        {
            string address = args.Length >= 2 ? args[1] : "127.0.0.1:5555";
            var (ip, port) = ParseAddress(address);
            InitializeLogging();
            await new GameClient(ip, port).ConnectAndRunAsync();
            break;
        }
    }
}
else
{
    await RunInteractiveMenuAsync();
}

static GameConfig InitializeLogging()
{
    var config = new GameConfig();
    config.LoadConfig();
    var memLog = new MemoryLogger();
    var fileLog = new FileLogger(config.LogDirectory, config.PlayerName);
    GameLogger.Initialize(new CompositeLogger(memLog, fileLog));
    GameLogger.Log("Session started.");
    return config;
}

static (string ip, int port) ParseAddress(string address, int defaultPort = 5555)
{
    int colonIdx = address.LastIndexOf(':');
    if (colonIdx > 0 && int.TryParse(address[(colonIdx + 1)..], out int port))
        return (address[..colonIdx], port);
    return (address, defaultPort);
}

static async Task RunInteractiveMenuAsync()
{
    Console.CursorVisible = false;
    Console.Clear();
    Console.WriteLine("  Dungeon RPG");
    Console.WriteLine();
    Console.WriteLine("  [1] Host a game  (server, port 5555)");
    Console.WriteLine("  [2] Join a game  (client)");
    Console.WriteLine("  [3] Play locally");
    Console.WriteLine();

    var key = Console.ReadKey(intercept: true).Key;

    switch (key)
    {
        case ConsoleKey.D1:
        {
            InitializeLogging();
            var theme = new DungeonSelectionMenu().Display();
            Console.Clear();
            await new GameServer(theme, 5555).StartAsync();
            break;
        }
        case ConsoleKey.D2:
        {
            Console.CursorVisible = true;
            Console.Clear();
            Console.Write("Server address (default 127.0.0.1:5555): ");
            string? input = Console.ReadLine();
            Console.CursorVisible = false;
            var (ip, port) = ParseAddress(string.IsNullOrWhiteSpace(input) ? "127.0.0.1:5555" : input);
            InitializeLogging();
            await new GameClient(ip, port).ConnectAndRunAsync();
            break;
        }
        case ConsoleKey.D3:
        {
            var config = InitializeLogging();
            GameLogger.Log($"Local game started by {config.PlayerName}.");
            var theme = new DungeonSelectionMenu().Display();
            var game = new Game(theme, config.PlayerName, config.LogDirectory);
            game.DisplayMenu();
            game.Run();
            break;
        }
    }
}
