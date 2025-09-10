using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirePortal {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 4, 4 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "Add 3 random warriors to your hand",
            "Add 4 random warriors to your hand"
            },
            race = Race.Fire,
            genre = Genre.Underworld,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int amount = parameters.cardLevel == 0 ? 3 : 4;


        for (int i = 0; i < amount; i++) {
            WarriorStats stats = CardDatabase.GetRandomCardStats(CardRarity.None, CardType.Warrior);

            await parameters.hand.MoveNewCardToHand(stats, parameters.deck.transform.position);
        }
    }
}