using System.Threading.Tasks;
using UnityEngine;

public class GoldenCape : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When a friend is summoned, give it Immune for 1 turn";
        rarity = ItemManager.Rarity.Boss;
        return this;
    }

    public override void UseOnFriendSpawn(ItemTriggerParams parameters) {
        parameters.stats.ability.immune.Add();
    }
}