using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Reinforcement {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 3 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "Draw 2 cards",
            "Draw 3 cards"
            },
            race = Character.Race.Light,
            cardType = CardType.Spell,
        };
        stats.genre = (Character.Genre)Enum.Parse(typeof(Character.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int amount = parameters.cardLevel == 0 ? 2 : 3;
        for (int i = 0; i < amount; i++) {
            await parameters.deck.DrawCard(false);
        }
    }
}