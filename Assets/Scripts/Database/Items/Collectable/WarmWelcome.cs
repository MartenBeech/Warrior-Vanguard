using System.Threading.Tasks;
using UnityEngine;

public class WarmWelcome : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Apply 1 Burning to summoned enemies";
        rarity = ItemManager.Rarity.Normal;
        return this;
    }

    public override async Task UseAfterEnemySummon(ItemTriggerParams parameters) {
        GridManager gridManager = FindFirstObjectByType<GridManager>();
        Warrior warrior = gridManager.GetCellWarrior(parameters.gridIndex);
        warrior.stats.ability.burning.Add(1);
        await Task.Delay(0);
    }
}