using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CLASSNAMESPELL {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 0, 0 },
            rarity = CardRarity.None,
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "UNUPGRADED_DESCRIPTION",
            "UPGRADED_DESCRIPTION"
            },
            race = Warrior.Race.None,
            cardType = CardType.Spell,
        };
        stats.genre = (Warrior.Genre)Enum.Parse(typeof(Warrior.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        List<Task> asyncFunctions = new();

        parameters.target.UpdateWarriorUI();
        asyncFunctions.Add(parameters.floatingText.CreateFloatingText(parameters.target.transform, "TEXT", ColorPalette.ColorEnum.Purple));
        await Task.WhenAll(asyncFunctions);
    }
}