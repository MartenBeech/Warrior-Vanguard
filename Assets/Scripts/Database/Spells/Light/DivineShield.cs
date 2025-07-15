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
            "Give warrior immune for 1 turn",
            "Give warrior immune for 1 turn"
            },
            race = Character.Race.None,
            cardType = CardType.spell,
        };

        return stats;
    }

    //TODO: Implement immune ability
    // public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        // target.stats.ability.immune.Add(1);
        // await floatingText.CreateFloatingText(target.transform, "Immune 1", ColorPalette.ColorEnum.yellow);
    // }
}