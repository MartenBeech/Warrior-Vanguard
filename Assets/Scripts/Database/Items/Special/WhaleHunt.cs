using System.Threading.Tasks;
using UnityEngine;

public class WhaleHunt : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Start of combat: Summon a 10/10 Moby Richard for your opponent";
        return this;
    }

    public override async Task UseStartOfEnemyCombat(Summoner summoner) {
        await Task.Delay(0);
        summoner.stats.ability.summonMobyRichard.Add();
    }
}