using System.Threading.Tasks;
using UnityEngine;

public class AmuletOfForten : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Set all warriors' stats to 4/10";
        return this;
    }

    public override void UseOnWarriorSpawn(ItemTriggerParams parameters) {
        parameters.stats.SetStrength(4);
        parameters.stats.SetHealth(10);
    }
}