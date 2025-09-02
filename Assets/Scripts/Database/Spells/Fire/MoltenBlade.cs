using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MoltenBlade {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 1, 0 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friend +5 strength this turn",
            "Give a friend +5 strength this turn"
            },
            race = Race.Fire,
            cardType = CardType.Spell,
        };
        stats.genre = (Genre)Enum.Parse(typeof(Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        parameters.target.stats.tempStrength += 5;
        parameters.target.UpdateWarriorUI();
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Molten Blade", ColorPalette.ColorEnum.Yellow);
    }
}