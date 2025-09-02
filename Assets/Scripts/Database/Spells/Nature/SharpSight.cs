using System;
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
            race = Warrior.Race.Nature,
            cardType = CardType.Spell,
        };
        stats.genre = (Warrior.Genre)Enum.Parse(typeof(Warrior.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int value = parameters.cardLevel == 0 ? 1 : 2;
        parameters.target.stats.range += value;
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Eagle Eye", ColorPalette.ColorEnum.Green);
    }
}