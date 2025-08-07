using System.Collections.Generic;
using System.Threading.Tasks;

public class CatReflexes {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 2 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friendly warrior retaliate",
            "Give a friendly warrior retaliate"
            },
            race = Character.Race.Nature,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        parameters.target.stats.ability.retaliate.Add();
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, $"Cat Reflexes", ColorPalette.ColorEnum.Green);
    }
}