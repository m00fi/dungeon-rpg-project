using System.Reflection.Metadata.Ecma335;
using dungeonRPG.Dungeon.Cells;
using dungeonRPG.Dungeon;
using dungeonRPG.Entities;

namespace dungeonRPG.UI;

public class Display
{
    private int ScreenWidth => 82;
    private int SidePanelWidth => 60;

    public void Render(Room room, Player player, string? message, List<string> instructions)
    {
        Console.SetCursorPosition(0, 0);

        RenderMapAndSidePanel(room, player);
        if (player.IsInCombat)
            RenderCombatMenu(player);
        else
            RenderGroundItems(room, player);
        
        RenderMessageBar(message);
        RenderInstructions(instructions);
        
        for (int i = 0; i < 3; i++) Console.WriteLine(new string(' ', ScreenWidth));
    }

    private void RenderMapAndSidePanel(Room room, Player player)
    {
        List<(string Text, bool Highlight)> sidePanel = GenerateSidePanel(room, player);

        int mapDisplayHeight = Room.Height + 2; 
        int mapDisplayWidth = Room.Width + 2;
        int totalRows = Math.Max(mapDisplayHeight, sidePanel.Count);

        for (int y = 0; y < totalRows; y++)
        {
            RenderMapRow(y, mapDisplayHeight, mapDisplayWidth, room, player);
            RenderSidePanelRow(y, sidePanel);
            Console.WriteLine();
        }
    }

    private void RenderGroundItems(Room room, Player player)
    {
        Cell currentCell = room.GetCell(player.X, player.Y);
        var itemDescriptions = currentCell.GetItemDescriptions();
        
        int maxVisibleGround = 5;

        string groundTitle = itemDescriptions.Count > maxVisibleGround 
            ? $"Available items to pick up (Total: {itemDescriptions.Count}):" 
            : "Available items to pick up:";
        Console.WriteLine(groundTitle.PadRight(ScreenWidth));

        if (itemDescriptions.Count == 0)
        {
            RenderEmptyItemList(player.IsInventoryActive, maxVisibleGround);
        }
        else
        {
            RenderPopulatedItemList(itemDescriptions, player.SelectedItemIndex, player.IsInventoryActive, maxVisibleGround);
        }
    }
    
    private void RenderCombatMenu(Player player)
    {
        var enemy = player.ActiveEnemy;
        if (enemy == null) return;

        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.ForegroundColor = ConsoleColor.White;

        Console.Write($"ENEMY APPROACHED:");
        Console.ResetColor();
        //Console.WriteLine($"".PadRight(ScreenWidth));
        Console.Write($" ({enemy.Symbol}) {enemy.Name}");
        Console.WriteLine($" | HP: {enemy.Health} | ATK: {enemy.Attack} | DEF: {enemy.Armor}".PadRight(ScreenWidth));
        
        Console.WriteLine(new string(' ', ScreenWidth - 40 + SidePanelWidth));
        Console.WriteLine(new string(' ', ScreenWidth - 40 + SidePanelWidth));
        Console.WriteLine("Choose your attack:".PadRight(ScreenWidth));
        Console.WriteLine(" [1] Normal Attack".PadRight(ScreenWidth));
        Console.WriteLine(" [2] Stealth Attack".PadRight(ScreenWidth));
        Console.WriteLine(" [3] Magic Attack".PadRight(ScreenWidth));
    }

    private void RenderMessageBar(string? message)
    {
        if (message != null)
        {
            //Console.Write("LOG:");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
            Console.ResetColor();
            
            int padding = ScreenWidth - 40 + SidePanelWidth - message.Length;
            if (padding > 0) Console.WriteLine(new string('-', padding));
        }
        else
        {
            // Console.BackgroundColor = ConsoleColor.DarkRed;
            // Console.ForegroundColor = ConsoleColor.White;
            //Console.Write("LOG:");
            // Console.ResetColor();
            Console.WriteLine(new string('-', ScreenWidth - 40 + SidePanelWidth));
        }
    }

    private void RenderInstructions(List<string> instructions)
    {
        foreach (var inst in instructions)
        {
            Console.WriteLine(inst.PadRight(ScreenWidth));
        }
    }

    private void RenderMapRow(int y, int mapDisplayHeight, int mapDisplayWidth, Room room, Player player)
    {
        if (y == 0 || y == mapDisplayHeight - 1)
        {
            Console.Write("+" + new string('-', Room.Width) + "+");
        }
        else if (y > 0 && y < mapDisplayHeight - 1)
        {
            int mapY = y - 1;
            Console.Write("|");

            for (int x = 0; x < Room.Width; x++)
            {
                if (x == player.X && mapY == player.Y)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(player.Symbol);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(room.GetCell(x, mapY).GetSymbol());
                }
            }
            Console.Write("|");
        }
        else
        {
            Console.Write(new string(' ', mapDisplayWidth));
        }
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
            Console.Write(new string(' ', 40));
        }
    }

    private void RenderEmptyItemList(bool isInventoryActive, int maxVisibleGround)
    {
        bool isSelected = !isInventoryActive;
        if (isSelected)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        
        string prefix = isSelected ? "> " : "  ";
        string textToPrint = $"{prefix}~empty ";
        Console.Write(textToPrint);
        Console.ResetColor();
        
        int padding = ScreenWidth - " ~empty".Length;
        if (padding > 0) Console.WriteLine(new string(' ', padding));
        else Console.WriteLine();
        
        for (int i = 1; i < maxVisibleGround; i++) 
        {
            Console.WriteLine(new string(' ', ScreenWidth));
        }
        Console.WriteLine(new string(' ', ScreenWidth));
    }

    private void RenderPopulatedItemList(List<string> itemDescriptions, int selectedItemIndex, bool isInventoryActive, int maxVisibleGround)
    {
        int startIdx = Math.Max(0, selectedItemIndex - (maxVisibleGround / 2));
        if (startIdx + maxVisibleGround > itemDescriptions.Count)
        {
            startIdx = Math.Max(0, itemDescriptions.Count - maxVisibleGround);
        }

        for (int i = 0; i < maxVisibleGround; i++)
        {
            int itemIdx = startIdx + i;
            if (itemIdx < itemDescriptions.Count)
            {
                bool isSelected = !isInventoryActive && (itemIdx == selectedItemIndex);
                
                if (isSelected)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                string prefix = isSelected ? "> " : "  ";
                string textToPrint = $"{prefix}{itemDescriptions[itemIdx]} ";
                
                Console.Write(textToPrint);
                Console.ResetColor();

                int padding = ScreenWidth - textToPrint.Length;
                if (padding > 0) Console.WriteLine(new string(' ', padding));
                else Console.WriteLine();
            }
            else
            {
                Console.WriteLine(new string(' ', ScreenWidth)); 
            }
        }

        if (itemDescriptions.Count > maxVisibleGround)
        {
            bool moreAbove = startIdx > 0;
            bool moreBelow = startIdx + maxVisibleGround < itemDescriptions.Count;
            
            string scrollIndicator = "  ";
            if (moreAbove && moreBelow) scrollIndicator += "^ (more items above and below) v";
            else if (moreAbove) scrollIndicator += "^ (more items above)";
            else if (moreBelow) scrollIndicator += "v (more items below) v";

            Console.WriteLine(scrollIndicator.PadRight(ScreenWidth));
        }
        else
        {
            Console.WriteLine(new string(' ', ScreenWidth)); 
        }
    }

    public string Separator() => new string('-', SidePanelWidth);

    private List<(string Text, bool Highlight)> GenerateSidePanel(Room room, Player player)
    {
        var panel = new List<(string, bool)>();

        void Add(string text, bool highlight = false) => panel.Add((text, highlight));
        string FormatRow(string left, string right) => left.PadRight(20) + right;

        Add(Separator());
        
        var stats = player.stats.GetAttributes();
        Add(FormatRow($"{player.Name}, Stats:", player.money.GetMoney()));
        
        foreach (var stat in stats)
        {
            Add(stat);
        }
        
        Add(Separator());
        Add($"Left hand: {player.equipment.GetLeftHandName()}");
        Add($"Right hand: {player.equipment.GetRightHandName()}");
        Add(Separator());
        
        var items = player.inventory.GetInventory();
        int maxVisible = 8;

        Add($"Inventory ({items.Count}/{player.inventory.Capacity}):");

        if (items.Count == 0)
        {
            bool isSelected = player.IsInventoryActive;
            string prefix = isSelected ? "> " : "  ";
            Add(prefix + "~empty", isSelected);
            for (int i = 1; i < maxVisible; i++) Add(new string(' ', SidePanelWidth)); 
        }
        else
        {
            int startIdx = Math.Max(0, player.SelectedInventoryIndex - (maxVisible / 2));
            if (startIdx + maxVisible > items.Count)
            {
                startIdx = Math.Max(0, items.Count - maxVisible);
            }

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

        Add(Separator());
        
        return panel;
    }
}