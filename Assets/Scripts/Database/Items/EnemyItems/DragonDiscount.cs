using System.Threading.Tasks;
using UnityEngine;

public class DragonDiscount : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Overturn: Reduce the cost of your dragons by 1";
        return this;
    }

    public override async Task UseStartOfTurn(ItemTriggerParams parameters) {
        parameters.enemyHand.ReduceCostRace(1, Character.Race.Dragon);
        await Task.CompletedTask;
    }
}