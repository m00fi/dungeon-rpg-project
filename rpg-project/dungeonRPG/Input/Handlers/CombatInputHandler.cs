using dungeonRPG.Combat;
using dungeonRPG.Dungeon;
using dungeonRPG.Entities;

namespace dungeonRPG.Input.Handlers;

public class CombatInputHandler : BaseInputHandler
{
    public override InputResult HandleInput(ConsoleKey key, Player player, Room room, List<ConsoleKey> activeKeys)
    {
        if (!player.IsInCombat || player.ActiveEnemy == null)
            return base.HandleInput(key, player, room, activeKeys);

        IAttackVisitor? visitor = null;

        switch (key)
        {
            case ConsoleKey.D1:
                visitor = new NormalAttackVisitor(player, player.ActiveEnemy);
                break;
                
            case ConsoleKey.D2:
                visitor = new StealthAttackVisitor(player, player.ActiveEnemy);
                break;
                
            case ConsoleKey.D3:
                visitor = new MagicAttackVisitor(player, player.ActiveEnemy);
                break;

            default:
                visitor = null;
                return new InputResult(false, "You cannot do this while in combat!");
        }

        if (visitor != null)
        {
            var weapon = player.equipment.RightHand; 
            
            if (weapon == null) 
                visitor.VisitUnarmed();
            else 
                weapon.Accept(visitor, weapon);

            string resultMessage = visitor.CombatMessage;

            if (player.ActiveEnemy.IsDead)
            {
                room.GetCell(player.X, player.Y).Enemy = null;
                player.ActiveEnemy = null;
                resultMessage += " You won the battle!";
            }
            else if (player.stats.Health <= 0)
            {
                return new InputResult(false, "YOU DIED! GAME OVER. Press any button to quit."); 
            }

            return new InputResult(false, resultMessage);
        }

        return base.HandleInput(key, player, room, activeKeys);
    }
}