namespace dungeonRPG.Dungeon.Cells;

using Entities;
public class EmptyCell : Cell
{
    public override char GetSymbol() => ' ';

    public override bool Enter(Player player)
    {
        
        return true;
    }
}