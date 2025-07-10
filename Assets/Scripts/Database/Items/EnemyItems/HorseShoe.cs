using System.Threading.Tasks;
using UnityEngine;

public class HorseShoe : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All warriors have +1 speed";
        return this;
    }

    public override void UseOnFriendSpawn(WarriorStats stats) {
        stats.speed++;
    }

    public override void UseOnEnemySpawn(WarriorStats stats) {
        stats.speed++;
    }
}