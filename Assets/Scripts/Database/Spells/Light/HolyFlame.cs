using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class HolyFlame {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Warrior,
            spellDescription = new string[] {
            "Deal 3 damage to a warrior",
            "Deal 4 damage to a warrior or 6 against undead"
            },
            race = Race.Light,
            cardType = CardType.Spell,
        };
        stats.genre = (Genre)Enum.Parse(typeof(Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int value = parameters.cardLevel == 0 ? 3 : 4;

        if (parameters.target.stats.genre == Genre.Undead) {
            int undeadValue = parameters.cardLevel == 0 ? 3 : 6;
            value = undeadValue;
        }

        await parameters.target.TakeDamage(parameters.target, value, DamageType.Magical);
        parameters.target.UpdateWarriorUI();
    }
}