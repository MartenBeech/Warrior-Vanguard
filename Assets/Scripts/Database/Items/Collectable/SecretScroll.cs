using System.Threading.Tasks;
using UnityEngine;

public class SecretScroll : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = $"Your spells cost 1 less";
        rarity = ItemRarity.Normal;
        return this;
    }

    public override void UseOnCardDrawn(ItemTriggerParams parameters) {
        if (parameters.card.stats.cardType == CardType.Spell) {
            parameters.card.stats.AddCost(-1);
            parameters.card.UpdateCardUI();
        }
    }
}