using System.Threading.Tasks;
using UnityEngine;

public class AgingCurse {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 3 },
            spellTarget = SpellTarget.Enemy,
            spellDescription = new string[] {
            "Reduce an enemy's strength, health and speed to half",
            "Reduce an enemy's strength, health and speed to half"
            },
            race = Character.Race.Dark,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        target.stats.AddStrength(-Mathf.FloorToInt(target.stats.GetStrength() / 2));
        target.stats.AddHealthCurrent(-Mathf.FloorToInt(target.stats.GetHealth() / 2));
        target.stats.speed /= 2;
        target.UpdateWarriorUI();
        await floatingText.CreateFloatingText(target.transform, "Aging", ColorPalette.ColorEnum.Purple);
    }
}