using System.Threading.Tasks;
using UnityEngine;

public class WarmWelcome : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Apply 1 Burning to summoned enemies";
        return this;
    }

    public override async Task UseAfterEnemySpawn(WarriorStats stats, Vector2 gridIndex) {
        GridManager gridManager = FindFirstObjectByType<GridManager>();
        Character character = gridManager.GetCellCharacter(gridIndex);
        character.stats.ability.burning.Add(1);
        await Task.Delay(0);
    }
}