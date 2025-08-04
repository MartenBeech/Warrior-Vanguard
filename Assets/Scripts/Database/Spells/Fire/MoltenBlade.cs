using System.Collections.Generic;
using System.Threading.Tasks;

public class MoltenBlade {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 1, 0 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friend +5 strength this turn",
            "Give a friend +5 strength this turn"
            },
            race = Character.Race.Fire,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        target.stats.tempStrength += 5;
        target.UpdateWarriorUI();
        await floatingText.CreateFloatingText(target.transform, "Molten Blade", ColorPalette.ColorEnum.Yellow);
    }
}