using System.Threading.Tasks;
using UnityEngine;

public class ShieldOfEnven : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Your summoner gains 1 Armor";
        rarity = ItemRarity.Normal;
        return this;
    }

    public override async Task UseStartOfCombat(ItemTriggerParams parameters) {
        parameters.summoner.stats.ability.armor.Add(1);
        await Task.Delay(0);
    }
}