using System.Threading.Tasks;
using UnityEngine;

public class BindingShackles : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Enemies are stunned when summoned";
        rarity = ItemRarity.Boss;
        return this;
    }

    public override void UseOnEnemySummon(ItemTriggerParams parameters) {
        parameters.stats.ability.stunned.Add();
    }
}