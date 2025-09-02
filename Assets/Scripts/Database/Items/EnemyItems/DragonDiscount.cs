using System.Threading.Tasks;
using UnityEngine;

public class DragonDiscount : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = $"{Keyword.Initiate}: Reduce the cost of your dragons by 1";
        rarity = ItemRarity.None;
        return this;
    }

    public override async Task UseStartOfTurn(ItemTriggerParams parameters) {
        parameters.enemyHand.ReduceCostRace(1, Race.Dragon);
        await Task.CompletedTask;
    }
}