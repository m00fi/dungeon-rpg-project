using dungeonRPG.Items;

namespace dungeonRPG.Entities;
using Dungeon;
using Dungeon.Cells;
using Modules;

public class Player
{
    public string Name { get; private set; }
    public char Symbol { get; private set; }

    public int X;
    public int Y;
    
    public Attribute stats = new Attribute();
    public Inventory inventory = new Inventory();
    public Money money = new Money();
    public Equipment equipment = new Equipment();

    public int SelectedItemIndex { get; private set; } = 0;
    public bool IsInventoryActive { get; private set; } = false;
    public int SelectedInventoryIndex { get; private set; } = 0;
    
    public Player(string name, int startX, int startY)
    {
        Symbol = '⁋';
        Name = name;
        if (Name == "Rogue") 
            Symbol = '%';
        X = startX;
        Y = startY;
    }

    public void Move(int dx, int dy)
    {
        X += dx;
        Y += dy;
    }

    public void TryMove(int dx, int dy, Room room)
    {
        int targetX = X + dx;
        int targetY = Y + dy;
        if (targetX < 0 || targetX >= 40 || targetY < 0 || targetY >= 20) return;

        Cell targetCell = room.GetCell(targetX, targetY);
        
        if (targetCell.Enter(this))
        {
            X = targetX;
            Y = targetY;
            SelectedItemIndex = 0;
        }
    }

    public void TryPickUp(Room room)
    {
        Cell currCell = room.GetCell(X, Y);
        IItem? item = currCell.GetItemAt(SelectedItemIndex);
        if(item == null || (item.IsInventoryItem && inventory.IsFullInventory())) 
            return;

        item = currCell.PopItemAt(SelectedItemIndex);
        if (item != null)
        {
            item.PickUp(this);
            if (SelectedItemIndex >= currCell.GetItemsCount() && SelectedItemIndex > 0)
            {
                SelectedItemIndex--;
            }
        }
    }
    
    public void TryDropItem(Room room)
    {
        if (!IsInventoryActive || inventory.Items.Count == 0) return;
        
        if (SelectedInventoryIndex >= 0 && SelectedInventoryIndex < inventory.Items.Count)
        {
            var itemToDrop = inventory.Items[SelectedInventoryIndex];
            inventory.Remove(itemToDrop);
            
            Cell currCell = room.GetCell(X, Y);
            currCell.AddItem(itemToDrop);
            
            if (SelectedInventoryIndex >= inventory.Items.Count && SelectedInventoryIndex > 0)
            {
                SelectedInventoryIndex--;
            }
        }
    }
    
    public void ToggleInventory()
    {
        IsInventoryActive = !IsInventoryActive;

        if (IsInventoryActive) SelectedInventoryIndex = 0;
        else SelectedItemIndex = 0;
    }
    
    public void SelectNextItem(Room room)
    {
        if (IsInventoryActive)
        {
            if (inventory.Items.Count > 0 && SelectedInventoryIndex < inventory.Items.Count - 1)
                SelectedInventoryIndex++;
        }
        else
        {
            Cell currCell = room.GetCell(X, Y);
            int maxItems = currCell.GetItemsCount();
            if (maxItems > 0 && SelectedItemIndex < maxItems - 1)
                SelectedItemIndex++;
        }
    }

    public void SelectPreviousItem()
    {
        if (IsInventoryActive)
        {
            if (SelectedInventoryIndex > 0) SelectedInventoryIndex--;
        }
        else
        {
            if (SelectedItemIndex > 0) SelectedItemIndex--;
        }
    }
    
    public void TryUseItem(Room room)
    {
        if (!IsInventoryActive || inventory.Items.Count == 0) return;

        if (SelectedInventoryIndex >= 0 && SelectedInventoryIndex < inventory.Items.Count)
        {
            var itemToUse = inventory.Items[SelectedInventoryIndex];
            itemToUse.Use(this, room);

            if (SelectedInventoryIndex >= inventory.Items.Count && SelectedInventoryIndex > 0)
            {
                SelectedInventoryIndex--;
            }
        }
    }
    
    public void TryUnequipAll(Room room)
    {
        var unequippedWeapons = equipment.UnequipAll();
        foreach (var oldWeapon in unequippedWeapons)
        {
            if (!inventory.TryAdd(oldWeapon))
            {
                var currentCell = room.GetCell(X, Y);
                currentCell.AddItem(oldWeapon);
            }
        }
    }
}