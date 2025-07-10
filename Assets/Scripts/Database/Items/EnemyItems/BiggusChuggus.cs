using System.Threading.Tasks;
using UnityEngine;

public class BiggusChuggusDoll : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Double the stats on all warriors";
        return this;
    }

    public override void UseOnFriendSpawn(WarriorStats stats) {
        stats.DoubleStats();
    }

    public override void UseOnEnemySpawn(WarriorStats stats) {
        stats.DoubleStats();
    }
}