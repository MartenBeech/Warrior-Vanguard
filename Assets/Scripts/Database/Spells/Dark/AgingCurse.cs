using System.Threading.Tasks;
using UnityEngine;

public class AgingCurse {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 3 },
            spellTarget = SpellTarget.enemy,
            spellDescription = new string[] {
            "Reduce an enemy's strength, health and speed to half",
            "Reduce an enemy's strength, health and speed to 1/3"
            },
            race = Character.Race.Dark,
            cardType = CardType.spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        int division = cardLevel == 0 ? 2 : 3;
        target.stats.AddStrength(-Mathf.FloorToInt(target.stats.GetStrength() / division));
        target.stats.AddHealthCurrent(-Mathf.FloorToInt(target.stats.GetHealth() / division));
        target.stats.speed /= division;
        target.UpdateWarriorUI();
        await floatingText.CreateFloatingText(target.transform, "Aging", ColorPalette.ColorEnum.purple);
    }
}