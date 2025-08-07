using System.Collections.Generic;
using System.Threading.Tasks;

public class AidFromTheSpirits {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 7, 7 },
            rarity = CardRarity.Legendary,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friendly warrior +8/+8",
            "Give a friendly warrior +8/+8 and spikes 2"
            },
            race = Character.Race.Nature,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        parameters.target.stats.AddStrength(8);
        parameters.target.stats.AddHealth(8);
        parameters.target.UpdateWarriorUI();

        if (parameters.cardLevel > 0) {
            parameters.target.stats.ability.spikes.Add(2);
        }

        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Aid From The Spirits", ColorPalette.ColorEnum.Green);
    }
}