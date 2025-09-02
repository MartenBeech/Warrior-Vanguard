using System;
using System.Threading.Tasks;

public class PoisonPotion {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 1, 1 },
            spellTarget = SpellTarget.Enemy,
            spellDescription = new string[] {
            "Apply 2 poison to an enemy",
            "Apply 3 poison to an enemy"
            },
            race = Warrior.Race.Dark,
            cardType = CardType.Spell,
        };
        stats.genre = (Warrior.Genre)Enum.Parse(typeof(Warrior.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int value = parameters.cardLevel == 0 ? 2 : 3;
        parameters.target.stats.ability.poisoned.Add(value);
        parameters.target.UpdateWarriorUI();
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, $"{value} poison", ColorPalette.ColorEnum.Purple);
    }
}