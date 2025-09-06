using System.Threading.Tasks;
using UnityEngine;

public class LuckyCoin : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Start each combat with 1 extra coin";
        rarity = ItemRarity.Boss;
        return this;
    }

    public override async Task UseStartOfCombat(ItemTriggerParams parameters) {
        parameters.coin.GainCoins();
        await Task.Delay(0);
    }
}