using System.Collections.Generic;
using System.Threading.Tasks;

public class SummonAid {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 4 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "Draw 2 cards. Reduce their cost by 1",
            "Draw 2 cards. Reduce their cost by 2"
            },
            race = Character.Race.Nature,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int discount = parameters.cardLevel == 0 ? 1 : 2;

        for (int i = 0; i < 2; i++) {
            parameters.deck.deckList[0].AddCost(-discount);
            await parameters.deck.DrawCard(false);
        }
    }
}