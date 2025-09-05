using System;
using System.Threading.Tasks;
using UnityEngine;

public class AgingCurse {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 3 },
            spellTarget = SpellTarget.Enemy,
            spellDescription = new string[] {
            "Reduce an enemy's strength, health and speed to half",
            "Reduce an enemy's strength, health and speed to half"
            },
            race = Race.Dark,
            genre = Genre.Undead,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        parameters.target.stats.AddStrength(-Mathf.FloorToInt(parameters.target.stats.GetStrength() / 2));
        parameters.target.stats.AddHealthCurrent(-Mathf.FloorToInt(parameters.target.stats.GetHealthCurrent() / 2));
        parameters.target.stats.speed /= 2;
        parameters.target.UpdateWarriorUI();
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Aging", ColorEnum.Purple);
    }
}