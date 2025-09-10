using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Haste {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 1, 1 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friendly warrior +1 speed",
            "Give a friendly warrior +2 speed"
            },
            race = Race.Nature,
            genre = Genre.Elves,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int value = parameters.cardLevel == 0 ? 1 : 2;
        parameters.target.stats.speed += value;
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Haste", ColorEnum.Green);
    }
}