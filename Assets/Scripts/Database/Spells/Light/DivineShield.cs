using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DivineShield {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 2 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Make a friend immune for 1 turn",
            "Make a friend immune for 1 turn"
            },
            race = Character.Race.Light,
            cardType = CardType.Spell,
        };
        stats.genre = (Character.Genre)Enum.Parse(typeof(Character.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        parameters.target.stats.ability.immune.Add();
        parameters.target.UpdateWarriorUI();
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Immune", ColorPalette.ColorEnum.Yellow);
    }
}