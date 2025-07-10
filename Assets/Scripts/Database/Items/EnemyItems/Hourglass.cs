using System.Threading.Tasks;
using UnityEngine;

public class Hourglass : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All warriors have 1 speed and 1 range";
        return this;
    }

    public override void UseOnFriendSpawn(WarriorStats stats) {
        stats.speed = 1;
        stats.range = 1;
    }

    public override void UseOnEnemySpawn(WarriorStats stats) {
        stats.speed = 1;
        stats.range = 1;
    }
}