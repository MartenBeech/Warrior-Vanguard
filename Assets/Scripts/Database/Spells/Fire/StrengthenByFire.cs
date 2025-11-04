using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class StrengthenByFire {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 1, 1 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Warrior,
            spellDescription = new string[] {
            "Give a warrior Overturn: take 1 damage and gain 1 strength",
            "Give a warrior Overturn: take 1 damage and gain 2 strength"
            },
            race = Race.Fire,
            genre = Genre.Underworld,
            cardType = CardType.Spell,
        };


        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        parameters.target.stats.ability.strengthenByFireAbility.Add(1, 2);
        parameters.target.UpdateWarriorUI();
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Strengthen By Fire", ColorEnum.Red);
    }
}