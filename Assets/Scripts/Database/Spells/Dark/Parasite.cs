using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Parasite {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 10, 8 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.Enemy,
            spellDescription = new string[] {
            "Kill an enemy and summon an exact copy for your side",
            "Kill an enemy and summon an exact copy for your side"
            },
            race = Race.Dark,
            genre = Genre.Undead,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        List<Task> asyncFunctions = new();

        WarriorStats stats = new();
        stats.SetStats(parameters.target.stats);

        if (parameters.target.stats.alignment == Alignment.Enemy) {
            stats.alignment = Alignment.Friend;
        } else if (parameters.target.stats.alignment == Alignment.Friend) {
            stats.alignment = Alignment.Enemy;
        }

        asyncFunctions.Add(parameters.target.Die(parameters.target));

        asyncFunctions.Add(parameters.warriorSummoner.SummonRandomly(stats, parameters.target.transform.position));

        await Task.WhenAll(asyncFunctions);
    }
}