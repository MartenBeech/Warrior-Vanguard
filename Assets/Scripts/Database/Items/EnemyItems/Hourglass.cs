using System.Threading.Tasks;
using UnityEngine;

public class Hourglass : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All warriors have 1 speed and 1 range";
        rarity = ItemRarity.None;
        return this;
    }

    public override void UseOnWarriorSummon(ItemTriggerParams parameters) {
        parameters.stats.speed = 1;
        parameters.stats.range = 1;
    }
}