using System.Threading.Tasks;
using UnityEngine;

public class CopyCat : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When your opponent summons their first warrior, you summon a copy";
        rarity = ItemManager.Rarity.None;
        return this;
    }

    public override async Task UseAfterEnemySummon(ItemTriggerParams parameters) {
        if (triggeredThisCombat) return;
        triggeredThisCombat = true;

        WarriorSummoner warriorSummoner = FindFirstObjectByType<WarriorSummoner>();
        GridManager gridManager = FindFirstObjectByType<GridManager>();

        WarriorStats clone = new();
        clone.SetStats(parameters.stats);
        clone.alignment = parameters.stats.alignment == WarriorSummoner.Alignment.Friend ? WarriorSummoner.Alignment.Enemy : WarriorSummoner.Alignment.Friend;
        await warriorSummoner.SummonRandomly(clone, gridManager.GetCellPosition(parameters.gridIndex));
    }
}