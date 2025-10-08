using System.Threading.Tasks;
using UnityEngine;

public class ExcavationFossil : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Start combat with 3 Skeleton Bones";
        rarity = ItemRarity.Normal;
        genre = Genre.Undead;
        return this;
    }

    public override async Task UseStartOfCombat(ItemTriggerParams parameters) {
        parameters.summoner.stats.skeletonBones += 3;
        await Task.Delay(0);
    }
}