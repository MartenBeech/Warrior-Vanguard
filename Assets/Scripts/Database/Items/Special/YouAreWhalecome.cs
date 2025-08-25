using System.Threading.Tasks;
using UnityEngine;

public class YouAreWhalecome : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Start of combat: Summon a 2/3 Flot Sam";
        return this;
    }

    public override async Task UseStartOfCombat(Summoner summoner) {
        await Task.Delay(0);
        summoner.stats.ability.summonFlotSam.Add();
    }
}