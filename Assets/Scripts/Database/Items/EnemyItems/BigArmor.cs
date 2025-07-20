using System.Threading.Tasks;
using UnityEngine;

public class BigArmor : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Your summoner has 1 armor and 1 resistance";
        return this;
    }

    public override async Task UseStartOfCombat(Summoner summoner) {
        await Task.Delay(0);
        summoner.stats.ability.armor.Add(1);
        summoner.stats.ability.resistance.Add(1);
    }
}