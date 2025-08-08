using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Arise {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 3 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "Add 2 warriors to your hand that died this game",
            "Add 3 warriors to your hand that died this game"
            },
            race = Character.Race.Dark,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int amount = parameters.cardLevel == 0 ? 2 : 3;

        List<string> graveyard = new();
        foreach (var title in parameters.summoner.stats.graveyard) {
            graveyard.Add(title);
        }

        for (int i = 0; i < amount; i++) {
            string title = Rng.Entry(graveyard);
            if (title == null) break;

            graveyard.Remove(title);

            Type type = Type.GetType(title);
            object instance = Activator.CreateInstance(type);
            WarriorStats stats = (WarriorStats)type.GetMethod("GetStats")?.Invoke(instance, null);

            await parameters.hand.MoveNewCardToHand(stats, parameters.deck.transform.position);
        }
    }
}