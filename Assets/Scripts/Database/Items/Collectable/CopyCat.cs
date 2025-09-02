using System.Threading.Tasks;
using UnityEngine;

public class CopyCat : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When your opponent summons their first warrior, you summon a copy";
        rarity = ItemManager.Rarity.None;
        return this;
    }

    public override async Task UseAfterEnemySpawn(ItemTriggerParams parameters) {
        if (triggeredThisCombat) return;
        triggeredThisCombat = true;

        CharacterSpawner characterSpawner = FindFirstObjectByType<CharacterSpawner>();
        GridManager gridManager = FindFirstObjectByType<GridManager>();

        WarriorStats clone = new();
        clone.SetStats(parameters.stats);
        clone.alignment = parameters.stats.alignment == CharacterSpawner.Alignment.Friend ? CharacterSpawner.Alignment.Enemy : CharacterSpawner.Alignment.Friend;
        await characterSpawner.SpawnRandomly(clone, gridManager.GetCellPosition(parameters.gridIndex));
    }
}