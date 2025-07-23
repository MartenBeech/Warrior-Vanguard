using System.Collections.Generic;
using System.Threading.Tasks;

public class AidFromTheSpirits {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 7, 7 },
            rarity = CardRarity.Legendary,
            spellTarget = SpellTarget.friend,
            spellDescription = new string[] {
            "Give a friendly warrior +8/+8",
            "Give a friendly warrior +8/+8 and spikes 2"
            },
            race = Character.Race.Nature,
            cardType = CardType.spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        target.stats.AddStrength(8);
        target.stats.AddHealth(8);
        target.UpdateWarriorUI();

        if (cardLevel > 0) {
            target.stats.ability.spikes.Add(2);
        }

        await floatingText.CreateFloatingText(target.transform, "Aid From The Spirits", ColorPalette.ColorEnum.green);
    }
}