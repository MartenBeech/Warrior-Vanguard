using UnityEngine;
using System.Threading.Tasks;

public class Recycle : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When a friend dies, gain 2 shield";
        rarity = ItemRarity.Normal;
        genre = Genre.None;
        return this;
    }

    public override async Task UseOnFriendDeath(ItemTriggerParams parameters) {
        await parameters.summoner.AddShield(2);
    }
}