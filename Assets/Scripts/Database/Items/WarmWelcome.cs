using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

public class WarmWelcome : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Enemies take 1 magical damage when summoned";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override async Task UseAfterEnemySpawn(WarriorStats stats, Vector2 gridIndex) {

        GridManager gridManager = FindFirstObjectByType<GridManager>();
        Character character = gridManager.GetCellCharacter(gridIndex);
        await character.TakeDamage(character, 1, Character.DamageType.Magical);
    }
}