using dungeonRPG.Dungeon;
using dungeonRPG.Entities;

namespace dungeonRPG.Items.Weapons;

public abstract class TwoHandedWeapon : Weapon
{
    public override void Use(Player player, Room room)
    {
        player.inventory.Remove(this);

        List<Weapon> unequippedWeapons;
        unequippedWeapons = player.equipment.EquipTwoHanded(this);
        
        foreach (var oldWeapon in unequippedWeapons)
        {
            if (!player.inventory.TryAdd(oldWeapon))
            {
                var currentCell = room.GetCell(player.X, player.Y);
                currentCell.TryAddItem(oldWeapon);
            }
        }
    }
}