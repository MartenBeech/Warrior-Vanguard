using System.Threading.Tasks;
using UnityEngine;

public class UnfinishedPuzzle : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When a friend is summoned, double its max health";
        rarity = ItemManager.Rarity.Normal;
        return this;
    }

    public override void UseOnFriendSpawn(ItemTriggerParams parameters) {
        parameters.stats.AddHealthMax(parameters.stats.GetHealthMax());
    }
}