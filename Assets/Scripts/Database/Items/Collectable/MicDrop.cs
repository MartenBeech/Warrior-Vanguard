using System.Threading.Tasks;
using UnityEngine;

public class MicDrop : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When you play a warrior with cost equal to your max coins, draw a card";
        rarity = ItemManager.Rarity.Normal;
        return this;
    }

    public override async Task UseAfterFriendSpawn(ItemTriggerParams parameters) {
        if (parameters.stats.GetCost() == parameters.coin.coinsTotal) {
            await parameters.ownDeck.DrawCard(false);
        }
    }
}