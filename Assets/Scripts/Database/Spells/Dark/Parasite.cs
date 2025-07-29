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

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        List<Task> asyncFunctions = new();

        WarriorStats stats = new();
        stats.SetStats(target.stats);

        if (target.stats.alignment == CharacterSpawner.Alignment.Enemy) {
            stats.alignment = CharacterSpawner.Alignment.Friend;
        } else if (target.stats.alignment == CharacterSpawner.Alignment.Friend) {
            stats.alignment = CharacterSpawner.Alignment.Enemy;
        }

        asyncFunctions.Add(target.Die(target));

        asyncFunctions.Add(characterSpawner.SpawnRandomly(stats, target.transform.position));

        await Task.WhenAll(asyncFunctions);
    }
}