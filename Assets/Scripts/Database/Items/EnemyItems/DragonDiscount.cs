using System.Threading.Tasks;
using UnityEngine;

public class DragonDiscount : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = $"{WarriorAbility.Keywords.Initiate}: Reduce the cost of your dragons by 1";
        rarity = ItemManager.Rarity.None;
        return this;
    }

    public override async Task UseStartOfTurn(ItemTriggerParams parameters) {
        parameters.enemyHand.ReduceCostRace(1, Warrior.Race.Dragon);
        await Task.CompletedTask;
    }
}