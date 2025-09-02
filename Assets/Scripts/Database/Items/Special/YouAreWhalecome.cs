using System.Threading.Tasks;
using UnityEngine;

public class YouAreWhalecome : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Start of combat: Summon a 2/3 Flot Sam";
        rarity = ItemRarity.Special;
        return this;
    }

    public override async Task UseStartOfCombat(ItemTriggerParams parameters) {
        await Task.Delay(0);
        parameters.summoner.stats.ability.summonFlotSam.Add();
    }
}