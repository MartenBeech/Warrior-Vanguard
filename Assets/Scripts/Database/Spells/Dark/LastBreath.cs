using System.Collections.Generic;
using System.Threading.Tasks;

public class LastBreath {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 5, 4 },
            spellTarget = SpellTarget.Enemy,
            spellDescription = new string[] {
            "Set an enemy's health to 1",
            "Set an enemy's health to 1"
            },
            race = Character.Race.Dark,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        List<Task> asyncFunctions = new();

        target.stats.AddHealthCurrent(-(target.stats.GetHealth() - 1));
        target.UpdateWarriorUI();

        asyncFunctions.Add(floatingText.CreateFloatingText(target.transform, "Last Breath", ColorPalette.ColorEnum.Purple));
        await Task.WhenAll(asyncFunctions);
    }
}