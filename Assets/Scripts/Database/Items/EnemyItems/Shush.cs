using System.Threading.Tasks;
using UnityEngine;

public class Shush : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All warriors are summoned with no abilities";
        return this;
    }

    public override void UseOnFriendSpawn(ItemTriggerParams parameters) {
        parameters.stats.ability = new();
    }

    public override void UseOnEnemySpawn(ItemTriggerParams parameters) {
        parameters.stats.ability = new();
    }
}