using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SlowDown {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 2, 1 },
            rarity = CardRarity.Legendary,
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "Set all enemies’ speed to 0 until your next turn",
            "Set all enemies’ speed to 0 until your next turn"
            },
            race = Race.Nature,
            genre = Genre.Elves,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        List<Warrior> enemies = parameters.gridManager.GetEnemies(GameManager.turn);
        List<Task> asyncFunctions = new();
        foreach (Warrior enemy in enemies) {
            enemy.stats.tempSpeed = -1;
        }
        await Task.WhenAll(asyncFunctions);
    }
}