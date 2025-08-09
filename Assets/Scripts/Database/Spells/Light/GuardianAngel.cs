using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GuardianAngel {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 3 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friend: Revive",
            "Give a friend: Revive"
            },
            race = Character.Race.Light,
            cardType = CardType.Spell,
        };
        stats.genre = (Character.Genre)Enum.Parse(typeof(Character.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        parameters.target.stats.ability.revive.Add();
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Guardian Angel", ColorPalette.ColorEnum.Yellow);
    }
}