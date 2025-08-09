using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UnholyStorm {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 4 },
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "Reduce all enemies' strength by 2",
            "Reduce all enemies' strength by 3"
            },
            race = Character.Race.Dark,
            cardType = CardType.Spell,
        };
        stats.genre = (Character.Genre)Enum.Parse(typeof(Character.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        List<Character> enemies = parameters.gridManager.GetEnemies(GameManager.turn);
        List<Task> asyncFunctions = new();
        foreach (Character enemy in enemies) {
            if (enemy.stats.GetStrength() > 0) {
                int value = parameters.cardLevel == 0 ? 2 : 3;
                enemy.stats.AddStrength(-value);

                enemy.UpdateWarriorUI();
                asyncFunctions.Add(parameters.floatingText.CreateFloatingText(enemy.transform, $"-{value} Strength", ColorPalette.ColorEnum.Purple));
            }
        }
        await Task.WhenAll(asyncFunctions);
    }
}