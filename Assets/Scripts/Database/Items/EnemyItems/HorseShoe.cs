using System.Threading.Tasks;
using UnityEngine;

public class HorseShoe : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All warriors have +1 speed";
        return this;
    }

    public override void UseOnWarriorSpawn(ItemTriggerParams parameters) {
        parameters.stats.speed++;
    }
}