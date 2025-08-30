using System.Threading.Tasks;
using UnityEngine;

public class BigArmor : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Your summoner has 1 armor and 1 resistance";
        rarity = ItemManager.Rarity.None;
        return this;
    }

    public override async Task UseStartOfCombat(ItemTriggerParams parameters) {
        await Task.Delay(0);
        parameters.summoner.stats.ability.armor.Add(1);
        parameters.summoner.stats.ability.resistance.Add(1);
    }
}