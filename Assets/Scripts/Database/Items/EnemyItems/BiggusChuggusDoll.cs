using System.Threading.Tasks;
using UnityEngine;

public class BiggusChuggusDoll : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Double the stats on all warriors";
        return this;
    }

    public override void UseOnWarriorSpawn(ItemTriggerParams parameters) {
        parameters.stats.DoubleStats();
    }
}