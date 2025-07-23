using System.Collections.Generic;
using System.Threading.Tasks;

public class Haste {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 1, 1 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.friend,
            spellDescription = new string[] {
            "Give a friendly warrior +1 speed",
            "Give a friendly warrior +2 speed"
            },
            race = Character.Race.Nature,
            cardType = CardType.spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        int value = cardLevel == 0 ? 1 : 2;
        target.stats.speed += value;
        await floatingText.CreateFloatingText(target.transform, "Haste", ColorPalette.ColorEnum.green);
    }
}