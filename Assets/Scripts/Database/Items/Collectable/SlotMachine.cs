using System.Threading.Tasks;
using UnityEngine;

public class SlotMachine : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Your cards cannot cost more than 7";
        rarity = ItemRarity.Normal;
        return this;
    }

    public override void UseOnCardDrawn(ItemTriggerParams parameters) {
        if (parameters.card.stats.GetCost() > 7) {
            parameters.card.stats.SetCost(7);
            parameters.card.UpdateCardUI();
        }
    }
}