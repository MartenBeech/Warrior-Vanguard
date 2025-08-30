using System.Threading.Tasks;
using UnityEngine;

public class Hourglass : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All warriors have 1 speed and 1 range";
        rarity = ItemManager.Rarity.None;
        return this;
    }

    public override void UseOnWarriorSpawn(ItemTriggerParams parameters) {
        parameters.stats.speed = 1;
        parameters.stats.range = 1;
    }
}