using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Blind {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 1 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.Enemy,
            spellDescription = new string[] {
            "Reduce enemy range to 1",
            "Reduce enemy range to 1"
            },
            race = Race.Nature,
            genre = Genre.Forest,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        parameters.target.stats.range = 1;
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Blind", ColorEnum.Red);
    }
}