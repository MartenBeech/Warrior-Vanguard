using System.Threading.Tasks;
using UnityEngine;

public class VoodooDoll : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = $"When your summoner takes physical damage, deal 1 magical damage to the enemy summoner";
        rarity = ItemRarity.Normal;
        return this;
    }
}