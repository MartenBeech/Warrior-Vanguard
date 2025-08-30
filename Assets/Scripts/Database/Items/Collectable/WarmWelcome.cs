using System.Threading.Tasks;
using UnityEngine;

public class WarmWelcome : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Apply 1 Burning to summoned enemies";
        rarity = ItemManager.Rarity.Normal;
        return this;
    }

    public override async Task UseAfterEnemySpawn(ItemTriggerParams parameters) {
        GridManager gridManager = FindFirstObjectByType<GridManager>();
        Character character = gridManager.GetCellCharacter(parameters.gridIndex);
        character.stats.ability.burning.Add(1);
        await Task.Delay(0);
    }
}