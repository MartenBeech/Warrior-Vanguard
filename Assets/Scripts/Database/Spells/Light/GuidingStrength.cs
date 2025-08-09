using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GuidingStrength {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 1, 1 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friendly warrior +2 strength",
            "Give a friendly warrior +3 strength"
            },
            race = Character.Race.Light,
            cardType = CardType.Spell,
        };
        stats.genre = (Character.Genre)Enum.Parse(typeof(Character.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int value = parameters.cardLevel == 0 ? 2 : 3;
        parameters.target.stats.AddStrength(value);
        parameters.target.UpdateWarriorUI();
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Guiding Strength", ColorPalette.ColorEnum.Yellow);
    }
}