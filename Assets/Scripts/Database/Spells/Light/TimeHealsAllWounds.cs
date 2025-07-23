using System.Collections.Generic;
using System.Threading.Tasks;

public class TimeHealsAllWounds {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.friend,
            spellDescription = new string[] {
            "Give a friendly warrior: Regeneration 1",
            "Give a friendly warrior: Regeneration 2"
            },
            race = Character.Race.Light,
            cardType = CardType.spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        int value = cardLevel == 0 ? 1 : 2;
        target.stats.ability.regeneration.Add(value);
        await floatingText.CreateFloatingText(target.transform, $"Regeneration {value}", ColorPalette.ColorEnum.yellow);
    }
}