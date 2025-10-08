using System.Threading.Tasks;
using UnityEngine;

public class RuneStone : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = $"The first time your summoner takes 2+ damage each turn, reduce it to 1";
        rarity = ItemRarity.Normal;
        genre = Genre.None;
        return this;
    }

    public override async Task UseStartOfTurn(ItemTriggerParams parameters) {
        triggeredThisTurn = false;
        await Task.Delay(0);
    }
}