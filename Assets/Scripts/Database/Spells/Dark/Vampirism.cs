using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Vampirism {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 5, 5 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friend: Lifesteal. Set its health to 1",
            "Give a friend: Lifesteal"
            },
            race = Race.Dark,
            cardType = CardType.Spell,
        };
        stats.genre = (Genre)Enum.Parse(typeof(Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        parameters.target.stats.ability.lifeSteal.Add();
        if (parameters.cardLevel == 0) {
            parameters.target.stats.SetHealthCurrent(1);
        }
        parameters.target.UpdateWarriorUI();
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Vampirism", ColorEnum.Yellow);
    }
}