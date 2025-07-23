using System.Collections.Generic;
using System.Threading.Tasks;

public class HolyEmpower {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 2 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.friend,
            spellDescription = new string[] {
            "Give a friendly warrior +3/+2",
            "Give a friendly warrior +3/+2"
            },
            race = Character.Race.Light,
            cardType = CardType.spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        target.stats.AddStrength(3);
        target.stats.AddHealth(2);
        target.UpdateWarriorUI();
        await floatingText.CreateFloatingText(target.transform, "Holy Empower", ColorPalette.ColorEnum.yellow);
    }
}