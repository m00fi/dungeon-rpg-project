namespace dungeonRPG.Dungeon.Cells;
using Entities;

public class WallCell : Cell
{
    public override char GetSymbol() => '█';
    public override bool Enter(Player player) => false;
}