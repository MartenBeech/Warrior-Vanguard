using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Firebolt {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 2, 2 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Warrior,
            spellDescription = new string[] {
            "Deal 3 damage to a warrior",
            "Deal 4 damage to a warrior"
            },
            race = Race.Fire,
            genre = Genre.Underworld,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int value = parameters.cardLevel == 0 ? 3 : 4;
        await parameters.target.TakeDamage(parameters.target, value, DamageType.Magical);
        parameters.target.UpdateWarriorUI();
    }
}