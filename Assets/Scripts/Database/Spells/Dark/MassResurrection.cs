using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class MassResurrection {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 10, 10 },
            rarity = CardRarity.Legendary,
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "Resurrect your 3 highest cost friends that died this game",
            "Resurrect your 4 highest cost friends that died this game"
            },
            race = Race.Dark,
            genre = Genre.Undead,
            cardType = CardType.Spell,
        };


        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        List<Task> asyncFunctions = new();
        int amount = parameters.cardLevel == 0 ? 3 : 4;

        List<WarriorStats> graveyard = new();
        foreach (var warrior in parameters.summoner.stats.graveyard) {
            graveyard.Add(warrior);
        }

        graveyard.Sort((a, b) => b.cost[0].CompareTo(a.cost[0]));
        
        
        if (graveyard.Count < amount) {
            amount = graveyard.Count;
        }

        for (int i = 0; i < amount; i++) {
            if (graveyard[0] == null) break;
            string title = graveyard[0].title;

            graveyard.RemoveAt(0);

            Type type = Type.GetType(title);
            object instance = Activator.CreateInstance(type);
            WarriorStats stats = (WarriorStats)type.GetMethod("GetStats")?.Invoke(instance, null);
            stats.SetStats(stats);
            stats.alignment = parameters.summoner.stats.alignment;
            asyncFunctions.Add(parameters.warriorSummoner.SummonRandomly(stats, parameters.summoner.transform.position));
            await Task.WhenAll(asyncFunctions);
        }
    }
}