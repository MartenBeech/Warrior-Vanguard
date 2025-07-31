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
            race = Character.Race.Dark,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        List<Task> asyncFunctions = new();

        WarriorStats stats = new() {
            title = target.stats.title
        };
        stats.ResetStats();
        stats.alignment = target.stats.alignment;

        asyncFunctions.Add(target.Die(target));

        asyncFunctions.Add(characterSpawner.SpawnRandomly(stats, target.transform.position));

        await Task.WhenAll(asyncFunctions);
    }
}