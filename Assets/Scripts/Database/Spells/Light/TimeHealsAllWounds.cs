using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TimeHealsAllWounds {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 2, 2 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friendly warrior: Regeneration 1",
            "Give a friendly warrior: Regeneration 2"
            },
            race = Race.Light,
            genre = Genre.Human,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int value = parameters.cardLevel == 0 ? 1 : 2;
        parameters.target.stats.ability.regeneration.Add(value);
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, $"Regeneration {value}", ColorEnum.Yellow);
    }
}