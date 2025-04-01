using System.Collections.Generic;
using System.Threading.Tasks;

public class UnholyStorm {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 4,
            spellTarget = SpellTarget.none,
            spellDescription = new string[] {
            "Reduce all enemies' attack by 2",
            "Reduce all enemies' attack by 3"
            },
            cardType = CardType.spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText) {
        List<Character> enemies = gridManager.GetEnemies(GameManager.turn);
        List<Task> asyncFunctions = new();
        foreach (Character enemy in enemies) {
            int value = cardLevel == 0 ? 2 : 3;
            enemy.stats.AddStrength(-value);
            enemy.UpdateWarriorUI();
            asyncFunctions.Add(floatingText.CreateFloatingText(enemy.transform, $"-{value} Strength", ColorPalette.ColorEnum.purple));
        }
        await Task.WhenAll(asyncFunctions);
    }
}