using System.Collections.Generic;
using System.Threading.Tasks;

public class CatReflexes {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 2 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.friend,
            spellDescription = new string[] {
            "Give a friendly warrior retaliate",
            "Give a friendly warrior retaliate"
            },
            race = Character.Race.None,
            cardType = CardType.spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        target.stats.ability.retaliate.Add();
        await floatingText.CreateFloatingText(target.transform, $"Cat Reflexes", ColorPalette.ColorEnum.green);
    }
}