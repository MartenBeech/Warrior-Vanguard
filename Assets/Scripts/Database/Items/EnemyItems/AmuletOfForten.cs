using System.Threading.Tasks;
using UnityEngine;

public class AmuletOfForten : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Set all warriors' stats to 4/10";
        return this;
    }

    public override void UseOnFriendSpawn(WarriorStats stats) {
        stats.SetStrength(4);
        stats.SetHealth(10);
    }

    public override void UseOnEnemySpawn(WarriorStats stats) {
        stats.SetStrength(4);
        stats.SetHealth(10);
    }
}