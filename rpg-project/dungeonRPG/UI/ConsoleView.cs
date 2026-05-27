using dungeonRPG.Systems.Network.Data;

namespace dungeonRPG.UI;

public class ConsoleView : IGameView
{
    private const int ScreenWidth = 82;
    private const int SidePanelWidth = 60;
    private const int MapWidth = 40;
    private const int MapHeight = 20;

    public void Render(GameStateDto state, int localPlayerId)
    {
        Console.SetCursorPosition(0, 0);

        if (!state.Players.TryGetValue(localPlayerId, out var localPlayer))
            return;

        if (localPlayer.IsLogsOpen)
        {
            RenderAllLogs(state.AllLogs);
            return;
        }

        RenderMapAndSidePanel(state, localPlayer, localPlayerId);

        if (localPlayer.IsInCombat && localPlayer.ActiveEnemy != null)
            RenderCombatMenu(localPlayer.ActiveEnemy);
        else
            RenderGroundItems(localPlayer);

        RenderMessageBar(state.CurrentMessage);

        int cleanupIteration = 11;
        if (localPlayer.IsInstructionsOpen)
        {
            int count = RenderInstructions(state.Instructions);
            cleanupIteration -= count;
        }
        else
        {
            int count = RenderLastLogs(state.RecentLogs, 5);
            cleanupIteration -= count;
        }

        for (int i = 0; i < cleanupIteration; i++)
            Console.WriteLine(new string(' ', ScreenWidth));
    }

    private void RenderMapAndSidePanel(GameStateDto state, PlayerInfoDto localPlayer, int localPlayerId)
    {
        var sidePanel = GenerateSidePanel(localPlayer);

        int mapDisplayHeight = MapHeight + 2;
        int mapDisplayWidth = MapWidth + 2;
        int totalRows = Math.Max(mapDisplayHeight, sidePanel.Count);

        for (int y = 0; y < totalRows; y++)
        {
            RenderMapRow(y, mapDisplayHeight, mapDisplayWidth, state, localPlayerId);
            RenderSidePanelRow(y, sidePanel);
            Console.WriteLine();
        }
    }

    private void RenderMapRow(int y, int mapDisplayHeight, int mapDisplayWidth, GameStateDto state, int localPlayerId)
    {
        if (y == 0 || y == mapDisplayHeight - 1)
        {
            Console.Write("+" + new string('-', MapWidth) + "+");
        }
        else if (y > 0 && y < mapDisplayHeight - 1)
        {
            int mapY = y - 1;
            Console.Write("|");

            string row = mapY < state.MapRows.Length ? state.MapRows[mapY] : new string(' ', MapWidth);

            for (int x = 0; x < MapWidth; x++)
            {
                var playerAtCell = state.Players.Values.FirstOrDefault(p => p.X == x && p.Y == mapY);
                if (playerAtCell != null)
                {
                    if (playerAtCell.Id == localPlayerId)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(playerAtCell.Symbol);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(x < row.Length ? row[x] : ' ');
                }
            }

            Console.Write("|");
        }
        else
        {
            Console.Write(new string(' ', mapDisplayWidth));
        }
    }

    private void RenderGroundItems(PlayerInfoDto player)
    {
        var items = player.GroundItems;
        int maxVisible = 5;

        string title = items.Count > maxVisible
            ? $"Available items to pick up (Total: {items.Count}):"
            : "Available items to pick up:";
        Console.WriteLine(title.PadRight(ScreenWidth));

        if (items.Count == 0)
            RenderEmptyItemList(player.IsInventoryActive, maxVisible);
        else
            RenderPopulatedItemList(items, player.SelectedItemIndex, player.IsInventoryActive, maxVisible);
    }

    private void RenderCombatMenu(EnemyInfoDto enemy)
    {
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("ENEMY APPROACHED:");
        Console.ResetColor();
        Console.Write($" ({enemy.Symbol}) {enemy.Name}");
        Console.WriteLine($" | HP: {enemy.Health} | ATK: {enemy.Attack} | DEF: {enemy.Armor}".PadRight(ScreenWidth));

        Console.WriteLine(new string(' ', ScreenWidth - MapWidth + SidePanelWidth));
        Console.WriteLine(new string(' ', ScreenWidth - MapWidth + SidePanelWidth));
        Console.WriteLine("Choose your attack:".PadRight(ScreenWidth));
        Console.WriteLine(" [1] Normal Attack".PadRight(ScreenWidth));
        Console.WriteLine(" [2] Stealth Attack".PadRight(ScreenWidth));
        Console.WriteLine(" [3] Magic Attack".PadRight(ScreenWidth));
    }

    private void RenderMessageBar(string? message)
    {
        if (message != null)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
            Console.ResetColor();

            int padding = ScreenWidth - MapWidth + SidePanelWidth - message.Length;
            if (padding > 0) Console.WriteLine(new string('-', padding));
            else Console.WriteLine();
        }
        else
        {
            Console.WriteLine(new string('-', ScreenWidth - MapWidth + SidePanelWidth));
        }
    }

    private int RenderInstructions(List<string> instructions)
    {
        foreach (var inst in instructions)
        {
            string safeInst = inst;
            if (inst.Contains('\t'))
            {
                var parts = inst.Split('\t');
                safeInst = $"{parts[0].PadRight(8)}{parts[1]}";
            }
            Console.WriteLine(safeInst.PadRight(ScreenWidth));
        }
        return instructions.Count;
    }

    private int RenderLastLogs(List<string> recentLogs, int count)
    {
        Console.WriteLine("LAST LOGS:".PadRight(ScreenWidth));
        var logs = recentLogs.TakeLast(count).ToList();
        foreach (var log in logs)
            Console.WriteLine(log.PadRight(ScreenWidth));
        return logs.Count;
    }

    private void RenderAllLogs(List<string> allLogs)
    {
        Console.Clear();
        Console.WriteLine("ALL LOGS (PRESS [J] TO CLOSE):".PadRight(ScreenWidth));
        foreach (var log in allLogs)
            Console.WriteLine(log.PadRight(ScreenWidth));
    }

    private void RenderEmptyItemList(bool isInventoryActive, int maxVisible)
    {
        bool isSelected = !isInventoryActive;
        if (isSelected)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        string prefix = isSelected ? "> " : "  ";
        string text = $"{prefix}~empty ";
        Console.Write(text);
        Console.ResetColor();

        int padding = ScreenWidth - " ~empty".Length;
        if (padding > 0) Console.WriteLine(new string(' ', padding));
        else Console.WriteLine();

        for (int i = 1; i < maxVisible; i++)
            Console.WriteLine(new string(' ', ScreenWidth));
        Console.WriteLine(new string(' ', ScreenWidth));
    }

    private void RenderPopulatedItemList(List<string> items, int selectedIdx, bool isInventoryActive, int maxVisible)
    {
        int startIdx = Math.Max(0, selectedIdx - (maxVisible / 2));
        if (startIdx + maxVisible > items.Count)
            startIdx = Math.Max(0, items.Count - maxVisible);

        for (int i = 0; i < maxVisible; i++)
        {
            int itemIdx = startIdx + i;
            if (itemIdx < items.Count)
            {
                bool isSelected = !isInventoryActive && (itemIdx == selectedIdx);
                if (isSelected)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                string prefix = isSelected ? "> " : "  ";
                string text = $"{prefix}{items[itemIdx]} ";
                Console.Write(text);
                Console.ResetColor();

                int padding = ScreenWidth - text.Length;
                if (padding > 0) Console.WriteLine(new string(' ', padding));
                else Console.WriteLine();
            }
            else
            {
                Console.WriteLine(new string(' ', ScreenWidth));
            }
        }

        if (items.Count > maxVisible)
        {
            bool moreAbove = startIdx > 0;
            bool moreBelow = startIdx + maxVisible < items.Count;

            string indicator = "  ";
            if (moreAbove && moreBelow) indicator += "^ (more items above and below) v";
            else if (moreAbove) indicator += "^ (more items above)";
            else if (moreBelow) indicator += "v (more items below) v";

            Console.WriteLine(indicator.PadRight(ScreenWidth));
        }
        else
        {
            Console.WriteLine(new string(' ', ScreenWidth));
        }
    }

    private List<(string Text, bool Highlight)> GenerateSidePanel(PlayerInfoDto player)
    {
        var panel = new List<(string, bool)>();
        void Add(string text, bool highlight = false) => panel.Add((text, highlight));
        string Sep() => new string('-', SidePanelWidth);

        Add(Sep());
        Add(FormatRow($"{player.Name}, Stats:", player.MoneyLine));
        foreach (var stat in player.StatLines)
            Add(stat);

        Add(Sep());
        Add($"Left hand: {player.LeftHandDescription}");
        Add($"Right hand: {player.RightHandDescription}");
        Add(Sep());

        var items = player.InventoryItems;
        int maxVisible = 8;
        Add($"Inventory ({items.Count}/{player.InventoryCapacity}):");

        if (items.Count == 0)
        {
            bool isSelected = player.IsInventoryActive;
            string prefix = isSelected ? "> " : "  ";
            Add(prefix + "~empty", isSelected);
            for (int i = 1; i < maxVisible; i++)
                Add(new string(' ', SidePanelWidth));
        }
        else
        {
            int startIdx = Math.Max(0, player.SelectedInventoryIndex - (maxVisible / 2));
            if (startIdx + maxVisible > items.Count)
                startIdx = Math.Max(0, items.Count - maxVisible);

            for (int i = 0; i < maxVisible; i++)
            {
                int itemIdx = startIdx + i;
                if (itemIdx < items.Count)
                {
                    bool isSelected = player.IsInventoryActive && (itemIdx == player.SelectedInventoryIndex);
                    string prefix = isSelected ? "> " : "  ";
                    Add(prefix + items[itemIdx], isSelected);
                }
                else
                {
                    Add(new string(' ', SidePanelWidth));
                }
            }
        }

        Add(Sep());
        return panel;
    }

    private void RenderSidePanelRow(int y, List<(string Text, bool Highlight)> sidePanel)
    {
        if (y < sidePanel.Count)
        {
            var line = sidePanel[y];
            if (line.Highlight)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.Write(line.Text.PadRight(SidePanelWidth));
            Console.ResetColor();
        }
        else
        {
            Console.Write(new string(' ', SidePanelWidth));
        }
    }

    private static string FormatRow(string left, string right) => left.PadRight(20) + right;
}
