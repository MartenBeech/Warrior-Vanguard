using System.Threading.Tasks;
using UnityEngine;

public class EagleEye : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Friendly warriors have +2 range";
        return this;
    }

    public override void UseOnFriendSpawn(ItemTriggerParams parameters) {
        parameters.stats.range += 2;
    }
}