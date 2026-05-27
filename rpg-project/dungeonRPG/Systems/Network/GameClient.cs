using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using dungeonRPG.Input;
using dungeonRPG.Systems.Network.Data;
using dungeonRPG.UI;

namespace dungeonRPG.Systems.Network;

public class GameClient
{
    private readonly string _serverIp;
    private readonly int _serverPort;
    private readonly ConsoleView _view = new();
    private readonly PlayerController _controller = new();
    private readonly object _renderLock = new();
    private int _localPlayerId;

    public GameClient(string serverIp, int port = 5555)
    {
        _serverIp = serverIp;
        _serverPort = port;
    }

    public async Task ConnectAndRunAsync()
    {
        using var tcpClient = new TcpClient();
        try
        {
            await tcpClient.ConnectAsync(_serverIp, _serverPort);
        }            
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect to server at {_serverIp}:{_serverPort} - {ex.Message}");
            Console.CursorVisible = true;
            return;
        }

        var stream = tcpClient.GetStream();
        var writer = new StreamWriter(stream, Encoding.UTF8, leaveOpen: true)
        {
            AutoFlush = true,
            NewLine = "\n"
        };
        var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);

        // First packet is always WelcomeDto — tells us our player slot
        string? welcomeLine = await reader.ReadLineAsync();
        if (welcomeLine == null) return;

        WelcomeDto? welcome;
        try { welcome = JsonSerializer.Deserialize<WelcomeDto>(welcomeLine); }
        catch { return; }
        if (welcome == null) return;

        _localPlayerId = welcome.PlayerId;

        Console.CursorVisible = false;
        Console.Clear();

        using var cts = new CancellationTokenSource();
        var receiveTask = Task.Run(() => ReceiveStateAsync(reader, cts.Token));

        try
        {
            while (true)
            {
                var action = await _controller.ReadActionAsync();
                if (action == null) continue;

                string json = JsonSerializer.Serialize(action);

                try { await writer.WriteLineAsync(json); }
                catch (IOException) { break; }

                if (action.Action == PlayerActionType.Exit) break;
            }
        }
        finally
        {
            cts.Cancel();
            try { tcpClient.Close(); } catch { /* ignore */ }
            try { writer.Dispose(); } catch { /* ignore */ }
            try { reader.Dispose(); } catch { /* ignore */ }
            Console.CursorVisible = true;
            Console.Clear();
        }

        await receiveTask;
    }

    // -------------------------------------------------------------------------
    // Background receive loop
    // -------------------------------------------------------------------------

    private async Task ReceiveStateAsync(StreamReader reader, CancellationToken ct)
    {
        try
        {
            while (!ct.IsCancellationRequested)
            {
                string? line = await reader.ReadLineAsync(ct);
                if (line == null) break;

                GameStateDto? state;
                try { state = JsonSerializer.Deserialize<GameStateDto>(line); }
                catch { continue; }

                if (state == null) continue;

                lock (_renderLock)
                {
                    _view.Render(state, _localPlayerId);
                }
            }
        }
        catch (OperationCanceledException) { /* normal shutdown */ }
        catch (Exception ex) when (ex is IOException or ObjectDisposedException) { /* server closed */ }
    }
}
