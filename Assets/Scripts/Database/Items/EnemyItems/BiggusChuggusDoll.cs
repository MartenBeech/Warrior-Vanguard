using System.Threading.Tasks;
using UnityEngine;

public class BiggusChuggusDoll : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Double the stats on all warriors";
        rarity = ItemManager.Rarity.None;
        return this;
    }

    public override void UseOnWarriorSummon(ItemTriggerParams parameters) {
        parameters.stats.DoubleStats();
    }
}