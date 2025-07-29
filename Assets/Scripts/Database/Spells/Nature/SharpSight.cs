using System.Collections.Generic;
using System.Threading.Tasks;

public class SharpSight {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friendly warrior +1 range",
            "Give a friendly warrior +2 range"
            },
            race = Character.Race.Nature,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        int value = cardLevel == 0 ? 1 : 2;
        target.stats.range += value;
        await floatingText.CreateFloatingText(target.transform, "Eagle Eye", ColorPalette.ColorEnum.Green);
    }
}