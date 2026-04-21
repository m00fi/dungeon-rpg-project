using dungeonRPG.Dungeon.Generation.Strategies;
using dungeonRPG.Entities.Enemies;
using dungeonRPG.Items;
using dungeonRPG.Items.Weapons;

namespace dungeonRPG.Themes;

public interface IThemeFactory
{
    string GetMessage();
    IDungeonGenerationStrategy GetGenerationStrategy();
    Enemy GetRandomEnemy(Random random);
    Weapon GetRandomWeapon(Random random);
    Weapon CreateArtifact();
}