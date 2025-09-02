using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class HolyEmpower {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 2 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friendly warrior +3/+2",
            "Give a friendly warrior +3/+2"
            },
            race = Warrior.Race.Light,
            cardType = CardType.Spell,
        };
        stats.genre = (Warrior.Genre)Enum.Parse(typeof(Warrior.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        parameters.target.stats.AddStrength(3);
        parameters.target.stats.AddHealth(2);
        parameters.target.UpdateWarriorUI();
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Holy Empower", ColorPalette.ColorEnum.Yellow);
    }
}