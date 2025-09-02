using System.Threading.Tasks;
using UnityEngine;

public class HorseShoe : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All warriors have +1 speed";
        rarity = ItemRarity.None;
        return this;
    }

    public override void UseOnWarriorSummon(ItemTriggerParams parameters) {
        parameters.stats.speed++;
    }
}