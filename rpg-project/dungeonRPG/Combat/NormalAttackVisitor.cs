using dungeonRPG.Entities;
using dungeonRPG.Entities.Enemies;
using dungeonRPG.Items.Weapons;
using dungeonRPG.Items.Weapons.WeaponCategories;
using dungeonRPG.Logging;

namespace dungeonRPG.Combat;

public class NormalAttackVisitor : IAttackVisitor
{
    private Player _player;
    private Enemy _enemy;
    public string CombatMessage { get; private set; } = "";

    public NormalAttackVisitor(Player player, Enemy enemy)
    {
        _player = player;
        _enemy = enemy;
    }
    public void Visit(IHeavyWeapon categoryToken, Weapon statsSource)
    {
        int playerDamage = statsSource.Damage;
        int playerDefense = _player.stats.Strength + _player.stats.Luck; 
        
        ResolveCombatExchange(playerDamage, playerDefense);
    }

    public void Visit(ILightWeapon categoryToken, Weapon statsSource)
    {
        int playerDamage = statsSource.Damage;
        int playerDefense = _player.stats.Dexterity + _player.stats.Luck;
        
        ResolveCombatExchange(playerDamage, playerDefense);
    }

    public void Visit(IMagicWeapon categoryToken, Weapon statsSource)
    {
        int playerDamage = 1;
        int playerDefense = _player.stats.Dexterity + _player.stats.Luck;
        
        ResolveCombatExchange(playerDamage, playerDefense);
    }

    public void VisitUnarmed()
    {
        int playerDamage = 0;
        int playerDefense = _player.stats.Dexterity;
        
        ResolveCombatExchange(playerDamage, playerDefense);
    }

    private void ResolveCombatExchange(int playerDamage, int playerDefense)
    {
        int actualDamageToEnemy = Math.Max(1, playerDamage - _enemy.Armor);
        _enemy.Health -= actualDamageToEnemy;

        if (_enemy.IsDead)
        {
            CombatMessage = $"Normal Attack! You dealt {actualDamageToEnemy} DMG. {_enemy.Name} defeated!";
            GameLogger.Log($"{_player.Name} dealt {actualDamageToEnemy} DMG to {_enemy.Name}.");
            GameLogger.Log($"{_player.Name} defeated {_enemy.Name}.");
            return;
        }

        int actualDamageToPlayer = Math.Max(0, _enemy.Attack - playerDefense);
        _player.stats.Health -= actualDamageToPlayer;

        CombatMessage = $"Normal Attack! You dealt {actualDamageToEnemy} DMG. Received {actualDamageToPlayer} DMG!";
        GameLogger.Log($"{_player.Name} dealt {actualDamageToEnemy} DMG to {_enemy.Name}.");
        GameLogger.Log($"{_player.Name} received {actualDamageToPlayer} DMG from {_enemy.Name}.");
    }
}