using System.Threading.Tasks;
using UnityEngine;

public class LanternOfWisps : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Your summoner has “Overturn: summon a 1/1 wisp”";
        return this;
    }

    public override async Task UseStartOfCombat(Summoner summoner) {
        await Task.Delay(0);
        summoner.stats.ability.summonWisp.Add();
    }
}