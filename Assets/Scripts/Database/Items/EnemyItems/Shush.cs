using System.Threading.Tasks;
using UnityEngine;

public class Shush : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All warriors are summoned with no abilities";
        rarity = ItemRarity.None;
        return this;
    }

    public override void UseOnFriendSummon(ItemTriggerParams parameters) {
        parameters.stats.ability = new();
    }

    public override void UseOnEnemySummon(ItemTriggerParams parameters) {
        parameters.stats.ability = new();
    }
}