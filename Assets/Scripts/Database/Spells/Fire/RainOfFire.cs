using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RainOfFire {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 3 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "Deal 1 damage to all enemies",
            "Deal 2 damage to all enemies"
            },
            race = Character.Race.Fire,
            cardType = CardType.Spell,
        };
        stats.genre = (Character.Genre)Enum.Parse(typeof(Character.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int value = parameters.cardLevel == 0 ? 1 : 2;
        List<Character> enemies = parameters.gridManager.GetEnemies(GameManager.turn);
        List<Task> asyncFunctions = new();
        foreach (Character enemy in enemies) {
            asyncFunctions.Add(enemy.TakeDamage(enemy, value, Character.DamageType.Magical));
        }
        await Task.WhenAll(asyncFunctions);
    }
}