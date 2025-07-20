using System.Threading.Tasks;
using UnityEngine;

public class Shush : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All warriors are summoned with no abilities";
        return this;
    }

    public override void UseOnFriendSpawn(WarriorStats stats) {
        stats.ability = new();
    }

    public override void UseOnEnemySpawn(WarriorStats stats) {
        stats.ability = new();
    }
}