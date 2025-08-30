using System.Threading.Tasks;
using UnityEngine;

public class LanternOfWisps : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Your summoner has “Overturn: summon a 1/1 wisp”";
        rarity = ItemManager.Rarity.None;
        return this;
    }

    public override async Task UseStartOfCombat(ItemTriggerParams parameters) {
        await Task.Delay(0);
        parameters.summoner.stats.ability.summonWisp.Add();
    }
}