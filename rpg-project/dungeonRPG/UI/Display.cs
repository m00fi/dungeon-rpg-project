using dungeonRPG.Dungeon.Cells;

namespace dungeonRPG.UI;

using Dungeon;
using Entities;

public class Display
{
    public void Render(Room room, Player player)
    {
        Console.SetCursorPosition(0, 0);

        List<string> sidePanel = GenerateSidePanel(room, player);

        int mapDisplayHeight = Room.Height + 2; 
        int mapDisplayWidth = Room.Width + 4;

        int totalRows = Math.Max(mapDisplayHeight, sidePanel.Count);

        for (int y = 0; y < totalRows; y++)
        {
            if (y == 0 || y == mapDisplayHeight - 1)
            {
                Console.Write(new string('░', mapDisplayWidth));
            }
            else if (y > 0 && y < mapDisplayHeight - 1)
            {
                int mapY = y - 1;
                
                Console.Write("░░");

                for (int x = 0; x < Room.Width; x++)
                {
                    if(x == player.X && mapY == player.Y)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write('¶');
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(room.GetCell(x, mapY).GetSymbol());
                    }
                }
                Console.Write("░░");
            }
            else
            {
                Console.Write(new string(' ', mapDisplayWidth));
            }

            Console.Write("     ");
            
            if (y < sidePanel.Count)
            {
                Console.Write(sidePanel[y].PadRight(40)); 
            }
            else
            {
                Console.Write(new string(' ', 40));
            }
            Console.WriteLine();
        }
    }

    public string Separator()
    {
        return new string('=', 40);
    }

    private List<string> GenerateSidePanel(Room room, Player player)
    {
        var panel = new List<string>();

        panel.Add(Separator());
        panel.Add($"{player.Name}, Stats:");
        panel.AddRange(player.stats.GetAttributes());

        panel.Add(Separator());

        panel.Add(player.money.GetMoney());
        
        panel.Add(Separator());

        panel.Add("Left hand: ~empty");
        panel.Add("Right hand: ~empty");
        panel.Add(Separator());

        panel.Add("Inventory:");
        panel.AddRange(player.inventory.GetInventory());
        panel.Add(Separator());
        panel.Add("Available actions:");
        
        Cell currentCell = room.GetCell(player.X, player.Y);
        string? itemNameOnGround = currentCell.GetTopItemName();
        if (player.inventory.Items.Count != 0)
        {
            panel.Add($"[I] Manage Inventory");
        }
        
        if (!string.IsNullOrEmpty(itemNameOnGround))
        {
            panel.Add($"[E] Pick-up: {itemNameOnGround}");
        }
        panel.Add(Separator());
        panel.Add($"X{player.X},Y{player.Y}");
        return panel;
    }
}