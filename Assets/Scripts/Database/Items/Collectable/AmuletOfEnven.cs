using System.Threading.Tasks;
using UnityEngine;

public class AmuletOfEnven : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Your summoner gains 2 Resistance";
        rarity = ItemRarity.Normal;
        genre = Genre.Elves;
        return this;
    }

    public override async Task UseStartOfCombat(ItemTriggerParams parameters) {
        parameters.summoner.stats.ability.armor.Add(1);
        await Task.Delay(0);
    }
}