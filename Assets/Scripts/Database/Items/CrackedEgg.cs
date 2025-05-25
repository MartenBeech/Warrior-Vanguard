using System.Threading.Tasks;
using UnityEngine;

public class CrackedEgg : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When you summon a 5+ cost warrior, it summons a 1 cost warrior";
        return this;
    }

    public override async Task UseAfterFriendSpawn(WarriorStats stats, Vector2 gridIndex) {
        if (stats.GetCost() >= 5) {
            CharacterSpawner characterSpawner = FindFirstObjectByType<CharacterSpawner>();
            WarriorStats random1Cost = CardDatabase.GetRandomWarriorWithSpecificCost(1);
            random1Cost.alignment = stats.alignment;
            GridManager gridManager = FindFirstObjectByType<GridManager>();
            Vector2 from = gridManager.GetCellPosition(gridIndex);

            await characterSpawner.SpawnRandomly(random1Cost, from);
        }
    }
}