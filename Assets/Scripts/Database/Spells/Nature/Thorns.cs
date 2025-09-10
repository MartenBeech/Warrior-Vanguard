using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Thorns {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 2, 2 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friendly warrior Spikes 2",
            "Give a friendly warrior Spikes 3"
            },
            race = Race.Nature,
            genre = Genre.Elves,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int value = parameters.cardLevel == 0 ? 2 : 3;
        parameters.target.stats.ability.spikes.Add(value);
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, $"Thorns", ColorEnum.Green);
    }
}