using System.Collections.Generic;
using System.Threading.Tasks;

public class Embiggen {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 1, 1 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friendly warrior +2 health",
            "Give a friendly warrior +3 health"
            },
            race = Character.Race.Nature,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        int value = cardLevel == 0 ? 2 : 3;
        target.stats.AddHealth(value);
        target.UpdateWarriorUI();
        await floatingText.CreateFloatingText(target.transform, "Embiggen", ColorPalette.ColorEnum.Green);
    }
}