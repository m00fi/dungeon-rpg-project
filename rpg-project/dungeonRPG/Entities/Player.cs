using dungeonRPG.Entities.Enemies;
using dungeonRPG.Items;

namespace dungeonRPG.Entities;
using Dungeon;
using Dungeon.Cells;
using Modules;

public class Player
{
    public string Name { get; private set; }
    public char Symbol { get; set; }

    public int X;
    public int Y;
    
    public Attribute stats;
    public Inventory inventory = new Inventory();
    public Money money = new Money();
    public Equipment equipment = new Equipment();

    public Enemy? ActiveEnemy { get; set; } = null;
    public bool IsInCombat => ActiveEnemy != null;

    public int SelectedItemIndex { get; private set; } = 0;
    public bool IsInventoryActive { get; private set; } = false;
    public int SelectedInventoryIndex { get; private set; } = 0;
    
    public Player(string name, int startX, int startY)
    {
        Symbol = '¶';
        Name = name;
        X = startX;
        Y = startY;

        stats = new Attribute(equipment);
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

    public string? TryPickUp(Room room)
    {
        Cell currCell = room.GetCell(X, Y);
        IItem? item = currCell.GetItemAt(SelectedItemIndex);
        
        if(item == null) return "No items to pick up.";
        if(item.IsInventoryItem && inventory.IsFullInventory()) return "Inventory is full!";

        item = currCell.PopItemAt(SelectedItemIndex);
        if (item != null)
        {
            item.PickUp(this);
            if (SelectedItemIndex >= currCell.GetItemsCount() && SelectedItemIndex > 0)
            {
                SelectedItemIndex--;
            }
        }

        return null;
    }
    
    public string? TryDropItem(Room room)
    {
        if (!IsInventoryActive || inventory.Items.Count == 0) return "No items to drop.";
        
        if (SelectedInventoryIndex >= 0 && SelectedInventoryIndex < inventory.Items.Count)
        {
            var itemToDrop = inventory.Items[SelectedInventoryIndex];
            inventory.Remove(itemToDrop);
            
            Cell currCell = room.GetCell(X, Y);
            currCell.TryAddItem(itemToDrop);
            
            if (SelectedInventoryIndex >= inventory.Items.Count && SelectedInventoryIndex > 0)
            {
                SelectedInventoryIndex--;
            }
        }

        return null;
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
    
    public string? TryUseItem(Room room)
    {
        if (!IsInventoryActive || inventory.Items.Count == 0) return "No items to use.";

        if (SelectedInventoryIndex >= 0 && SelectedInventoryIndex < inventory.Items.Count)
        {
            var itemToUse = inventory.Items[SelectedInventoryIndex];
            itemToUse.Use(this, room);

            if (SelectedInventoryIndex >= inventory.Items.Count && SelectedInventoryIndex > 0)
            {
                SelectedInventoryIndex--;
            }
        }

        return null;
    }
    
    public string? TryUnequipAll(Room room)
    {
        var unequippedWeapons = equipment.UnequipAll();
        
        if(unequippedWeapons.Count == 0)
            return "No items to unequip.";
        
        foreach (var oldWeapon in unequippedWeapons)
        {
            if (!inventory.TryAdd(oldWeapon))
            {
                var currentCell = room.GetCell(X, Y);
                currentCell.TryAddItem(oldWeapon);
            }
        }

        return null;
    }
}