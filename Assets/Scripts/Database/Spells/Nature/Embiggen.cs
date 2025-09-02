using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Embiggen {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 1, 1 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friendly warrior +2 health",
            "Give a friendly warrior +3 health"
            },
            race = Warrior.Race.Nature,
            cardType = CardType.Spell,
        };
        stats.genre = (Warrior.Genre)Enum.Parse(typeof(Warrior.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int value = parameters.cardLevel == 0 ? 2 : 3;
        parameters.target.stats.AddHealth(value);
        parameters.target.UpdateWarriorUI();
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Embiggen", ColorPalette.ColorEnum.Green);
    }
}