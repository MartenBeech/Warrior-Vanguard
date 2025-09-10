using System;
using System.Threading.Tasks;

public class PoisonPotion {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 1, 1 },
            spellTarget = SpellTarget.Enemy,
            spellDescription = new string[] {
            "Apply 2 poison to an enemy",
            "Apply 3 poison to an enemy"
            },
            race = Race.Dark,
            genre = Genre.Undead,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int value = parameters.cardLevel == 0 ? 2 : 3;
        parameters.target.stats.ability.poisoned.Add(value);
        parameters.target.UpdateWarriorUI();
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, $"{value} poison", ColorEnum.Purple);
    }
}