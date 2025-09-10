using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Vampirism {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 5, 5 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friend: Lifesteal. Set its health to 1",
            "Give a friend: Lifesteal"
            },
            race = Race.Dark,
            genre = Genre.Undead,
            cardType = CardType.Spell,
        };

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