using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DivineShield {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 3, 2 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Make a friend immune for 1 turn",
            "Make a friend immune for 1 turn"
            },
            race = Race.Light,
            genre = Genre.Human,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        parameters.target.stats.ability.immune.Add();
        parameters.target.UpdateWarriorUI();
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Divine Shield", ColorEnum.Yellow);
    }
}