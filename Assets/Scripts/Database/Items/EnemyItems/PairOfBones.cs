using System.Threading.Tasks;
using UnityEngine;

public class PairOfBones : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Starts with 2 Skeleton Bones";
        return this;
    }


    public override async Task UseStartOfCombat(Summoner summoner) {
        await Task.Delay(0);
        summoner.stats.skeletonBones = 2;
    }
}