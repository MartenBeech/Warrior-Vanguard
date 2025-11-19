using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ElvenTechniques {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 7, 6 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friendly warrior Double Strike",
            "Give a friendly warrior Double Strike"
            },
            race = Race.Nature,
            genre = Genre.Elves,
            cardType = CardType.Spell,
        };


        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        parameters.target.stats.ability.doubleStrike.Add();

        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Elven Techniques", ColorEnum.Green);
    }
}