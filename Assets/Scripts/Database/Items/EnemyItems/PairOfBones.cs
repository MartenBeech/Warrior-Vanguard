using System.Threading.Tasks;
using UnityEngine;

public class PairOfBones : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Starts with 2 Skeleton Bones";
        rarity = ItemRarity.None;
        return this;
    }


    public override async Task UseStartOfCombat(ItemTriggerParams parameters) {
        await Task.Delay(0);
        parameters.summoner.stats.skeletonBones = 2;
    }
}