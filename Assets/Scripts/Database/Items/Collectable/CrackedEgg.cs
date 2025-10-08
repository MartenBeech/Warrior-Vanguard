using System.Threading.Tasks;
using UnityEngine;

public class CrackedEgg : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When you summon a 5+ cost warrior, it summons a 1 cost warrior";
        rarity = ItemRarity.Normal;
        genre = Genre.None;
        return this;
    }

    public override async Task UseAfterFriendSummon(ItemTriggerParams parameters) {
        if (parameters.stats.GetCost() >= 5) {
            WarriorSummoner warriorSummoner = FindFirstObjectByType<WarriorSummoner>();
            WarriorStats random1Cost = CardDatabase.GetRandomWarriorWithSpecificCost(1, parameters.stats.alignment);
            GridManager gridManager = FindFirstObjectByType<GridManager>();
            Vector2 from = gridManager.GetCellPosition(parameters.gridIndex);

            await warriorSummoner.SummonRandomly(random1Cost, from);
        }
    }
}