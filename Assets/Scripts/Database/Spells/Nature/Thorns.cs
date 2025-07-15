using System.Collections.Generic;
using System.Threading.Tasks;

public class Thorns {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.friend,
            spellDescription = new string[] {
            "Give a friendly warrior Spikes 2",
            "Give a friendly warrior Spikes 3"
            },
            race = Character.Race.None,
            cardType = CardType.spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        int value = cardLevel == 0 ? 2 : 3;
        target.stats.ability.spikes.Add(value);
        await floatingText.CreateFloatingText(target.transform, $"Thorns", ColorPalette.ColorEnum.green);
    }
}