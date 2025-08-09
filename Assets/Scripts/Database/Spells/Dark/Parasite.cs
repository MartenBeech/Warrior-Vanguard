using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Parasite {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 10, 8 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.Enemy,
            spellDescription = new string[] {
            "Kill an enemy and summon an exact copy for your side",
            "Kill an enemy and summon an exact copy for your side"
            },
            race = Character.Race.Dark,
            cardType = CardType.Spell,
        };
        stats.genre = (Character.Genre)Enum.Parse(typeof(Character.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        List<Task> asyncFunctions = new();

        WarriorStats stats = new();
        stats.SetStats(parameters.target.stats);

        if (parameters.target.stats.alignment == CharacterSpawner.Alignment.Enemy) {
            stats.alignment = CharacterSpawner.Alignment.Friend;
        } else if (parameters.target.stats.alignment == CharacterSpawner.Alignment.Friend) {
            stats.alignment = CharacterSpawner.Alignment.Enemy;
        }

        asyncFunctions.Add(parameters.target.Die(parameters.target));

        asyncFunctions.Add(parameters.characterSpawner.SpawnRandomly(stats, parameters.target.transform.position));

        await Task.WhenAll(asyncFunctions);
    }
}