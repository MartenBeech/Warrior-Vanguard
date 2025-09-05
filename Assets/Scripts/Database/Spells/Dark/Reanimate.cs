using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Reanimate {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 3 },
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Kill a friend and resummon it",
            "Kill a friend and resummon it"
            },
            race = Race.Dark,
            genre = Genre.Undead,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        List<Task> asyncFunctions = new();

        WarriorStats stats = new() {
            title = parameters.target.stats.title
        };
        stats.ResetStats();
        stats.alignment = parameters.target.stats.alignment;

        asyncFunctions.Add(parameters.target.Die(parameters.target));

        asyncFunctions.Add(parameters.warriorSummoner.SummonRandomly(stats, parameters.target.transform.position));

        await Task.WhenAll(asyncFunctions);
    }
}