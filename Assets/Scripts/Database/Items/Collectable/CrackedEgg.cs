using System.Threading.Tasks;
using UnityEngine;

public class CrackedEgg : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When you summon a 5+ cost warrior, it summons a 1 cost warrior";
        rarity = ItemManager.Rarity.Normal;
        return this;
    }

    public override async Task UseAfterFriendSpawn(ItemTriggerParams parameters) {
        if (parameters.stats.GetCost() >= 5) {
            CharacterSpawner characterSpawner = FindFirstObjectByType<CharacterSpawner>();
            WarriorStats random1Cost = CardDatabase.GetRandomWarriorWithSpecificCost(1, parameters.stats.alignment);
            GridManager gridManager = FindFirstObjectByType<GridManager>();
            Vector2 from = gridManager.GetCellPosition(parameters.gridIndex);

            await characterSpawner.SpawnRandomly(random1Cost, from);
        }
    }
}