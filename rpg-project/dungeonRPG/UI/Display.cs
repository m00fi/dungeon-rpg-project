using dungeonRPG.Dungeon.Cells;
using dungeonRPG.Dungeon;
using dungeonRPG.Entities;

namespace dungeonRPG.UI;

public class Display
{
    private int ScreenWidth => 82;
    private int ScreenHeight => 0;
    
    public void Render(Room room, Player player, string? message, List<string> instructions)
    {
        Console.SetCursorPosition(0, 0);

        List<(string Text, bool Highlight)> sidePanel = GenerateSidePanel(room, player);

        int mapDisplayHeight = Room.Height + 2; 
        int mapDisplayWidth = Room.Width + 2;

        int totalRows = Math.Max(mapDisplayHeight, sidePanel.Count);

        for (int y = 0; y < totalRows; y++)
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
                    if(x == player.X && mapY == player.Y)
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

            if (y < sidePanel.Count)
            {
                var line = sidePanel[y];
                
                if (line.Highlight)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.Write(line.Text.PadRight(40)); 
                Console.ResetColor();
            }
            else
            {
                Console.Write(new string(' ', 40));
            }
            Console.WriteLine();
        }

        Cell currentCell = room.GetCell(player.X, player.Y);
        var itemDescriptions = currentCell.GetItemDescriptions();
        
        int maxVisibleGround = 5;

        string groundTitle = itemDescriptions.Count > maxVisibleGround 
            ? $"Available items to pick up (Total: {itemDescriptions.Count}):" 
            : "Available items to pick up:";
        Console.WriteLine(groundTitle.PadRight(ScreenWidth));

        if (itemDescriptions.Count == 0)
        {
            bool isSelected = !player.IsInventoryActive;
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
        else
        {
            int selectedIdx = player.SelectedItemIndex;
            
            int startIdx = Math.Max(0, selectedIdx - (maxVisibleGround / 2));
            if (startIdx + maxVisibleGround > itemDescriptions.Count)
            {
                startIdx = Math.Max(0, itemDescriptions.Count - maxVisibleGround);
            }

            for (int i = 0; i < maxVisibleGround; i++)
            {
                int itemIdx = startIdx + i;
                if (itemIdx < itemDescriptions.Count)
                {
                    bool isSelected = !player.IsInventoryActive && (itemIdx == selectedIdx);
                    
                    if (isSelected)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    string prefix = isSelected ? "> " : "  ";
                    string textToPrint = $"{prefix}{itemDescriptions[itemIdx]} ";
                    
                    Console.Write(textToPrint);
                    Console.ResetColor();

                    int padding = 80 - textToPrint.Length;
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
        if (message != null)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
            Console.ResetColor();
            int padding = ScreenWidth - message.Length;
            if (padding > 0) Console.WriteLine(new string('-', padding));
        }
        else
        {
            Console.WriteLine(new string('-', 82));
        }

        foreach (var inst in instructions)
        {
            Console.WriteLine(inst.PadRight(80));
        }
        
        for (int i = 0; i < 3; i++) Console.WriteLine(new string(' ', 80));
    }

    public string Separator()
    {
        return new string('-', 40);
    }

    private List<(string Text, bool Highlight)> GenerateSidePanel(Room room, Player player)
    {
        var panel = new List<(string, bool)>();

        void Add(string text, bool highlight = false) => panel.Add((text, highlight));
        
        string FormatRow(string left, string right) 
        {
            return left.PadRight(20) + right;
        }

        Add(Separator());
        
        var stats = player.stats.GetAttributes();

        Add(FormatRow($"{player.Name}, Stats:", player.money.GetMoney()));
        Add(stats[0]);
        Add(stats[1]);
        Add(stats[2]);
        Add(stats[3]);
        Add(stats[4]);
        Add(stats[5]);
        
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
            Add(prefix+ "~empty", isSelected);
            for (int i = 1; i < maxVisible; i++) Add(""); 
        }
        else
        {
            int selectedIdx = player.SelectedInventoryIndex;
            
            int startIdx = Math.Max(0, selectedIdx - (maxVisible / 2));
            if (startIdx + maxVisible > items.Count)
            {
                startIdx = Math.Max(0, items.Count - maxVisible);
            }

            for (int i = 0; i < maxVisible; i++)
            {
                int itemIdx = startIdx + i;
                if (itemIdx < items.Count)
                {
                    bool isSelected = player.IsInventoryActive && (itemIdx == selectedIdx);
                    string prefix = isSelected ? "> " : "  "; 
                    Add(prefix + items[itemIdx], isSelected);
                }
                else
                {
                    Add("");
                }
            }
        }

        Add(Separator());
        
        return panel;
    }
}