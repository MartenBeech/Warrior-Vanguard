using System.Threading.Tasks;
using UnityEngine;

public class Tactics : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Friends have +1 deployment area";
        rarity = ItemRarity.Normal;
        genre = Genre.None;
        return this;
    }

    public override void UseImmediately(ItemTriggerParams parameters) {
        FriendlySummoner.extraDeploymentArea += 1;
    }
}