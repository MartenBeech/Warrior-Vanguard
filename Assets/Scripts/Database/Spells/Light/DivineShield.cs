using System.Collections.Generic;
using System.Threading.Tasks;

public class DivineShield {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 2 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.friend,
            spellDescription = new string[] {
            "Make a friend immune for 1 turn",
            "Make a friend immune for 1 turn"
            },
            race = Character.Race.Light,
            cardType = CardType.spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        target.stats.ability.immune.Add();
        target.UpdateWarriorUI();
        await floatingText.CreateFloatingText(target.transform, "Immune", ColorPalette.ColorEnum.yellow);
    }
}