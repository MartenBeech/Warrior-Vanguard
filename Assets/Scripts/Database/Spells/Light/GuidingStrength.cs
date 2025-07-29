using System.Collections.Generic;
using System.Threading.Tasks;

public class GuidingStrength {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 1, 1 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friendly warrior +2 strength",
            "Give a friendly warrior +3 strength"
            },
            race = Character.Race.Light,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        int value = cardLevel == 0 ? 2 : 3;
        target.stats.AddStrength(value);
        target.UpdateWarriorUI();
        await floatingText.CreateFloatingText(target.transform, "Guiding Strength", ColorPalette.ColorEnum.Yellow);
    }
}