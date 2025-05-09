using System.Collections.Generic;
using System.Threading.Tasks;

public class UnholyStorm {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 4,
            spellTarget = SpellTarget.none,
            spellDescription = new string[] {
            "Reduce all enemies' strength by 2 (minimum 1)",
            "Reduce all enemies' strength by 3 (minimum 1)"
            },
            race = Character.Race.Dark,
            cardType = CardType.spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        List<Character> enemies = gridManager.GetEnemies(GameManager.turn);
        List<Task> asyncFunctions = new();
        foreach (Character enemy in enemies) {
            if (enemy.stats.GetStrength() > 0) {
                int value = cardLevel == 0 ? 2 : 3;
                enemy.stats.AddStrength(-value);
                if (enemy.stats.GetStrength() < 1) {
                    enemy.stats.AddStrength(1 - enemy.stats.GetStrength());
                }
                enemy.UpdateWarriorUI();
                asyncFunctions.Add(floatingText.CreateFloatingText(enemy.transform, $"-{value} Strength", ColorPalette.ColorEnum.purple));
            }
        }
        await Task.WhenAll(asyncFunctions);
    }
}