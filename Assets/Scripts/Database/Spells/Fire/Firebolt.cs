using System.Collections.Generic;
using System.Threading.Tasks;

public class Firebolt {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Warrior,
            spellDescription = new string[] {
            "Deal 3 damage to a warrior",
            "Deal 4 damage to a warrior"
            },
            race = Character.Race.Fire,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int value = parameters.cardLevel == 0 ? 3 : 4;
        await parameters.target.TakeDamage(parameters.target, value, Character.DamageType.Magical);
        parameters.target.UpdateWarriorUI();
    }
}