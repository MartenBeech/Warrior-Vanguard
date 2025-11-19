using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Arise {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 3, 3 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "Add 2 friends to your hand that died this game",
            "Add 3 friends to your hand that died this game"
            },
            race = Race.Dark,
            genre = Genre.Undead,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int amount = parameters.cardLevel == 0 ? 2 : 3;

        List<WarriorStats> graveyard = new();
        foreach (var warrior in parameters.summoner.stats.graveyard) {
            graveyard.Add(warrior);
        }

        for (int i = 0; i < amount; i++) {
            WarriorStats warrior = Rng.Entry(graveyard);
            if (warrior == null) break;

            graveyard.Remove(warrior);

            Type type = Type.GetType(warrior.title);
            object instance = Activator.CreateInstance(type);
            WarriorStats stats = (WarriorStats)type.GetMethod("GetStats")?.Invoke(instance, null);

            await parameters.hand.MoveNewCardToHand(stats, parameters.deck.transform.position);
        }
    }
}