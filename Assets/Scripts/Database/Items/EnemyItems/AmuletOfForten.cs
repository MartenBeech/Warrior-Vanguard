using System.Threading.Tasks;
using UnityEngine;

public class AmuletOfForten : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Set all warriors' stats to 4/10";
        rarity = ItemRarity.None;
        return this;
    }

    public override void UseOnWarriorSummon(ItemTriggerParams parameters) {
        parameters.stats.SetStrength(4);
        parameters.stats.SetHealth(10);
    }
}