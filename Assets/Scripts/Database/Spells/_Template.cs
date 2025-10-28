using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CLASSNAMESPELL {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 0, 0 },
            rarity = CardRarity.None,
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "UNUPGRADED_DESCRIPTION",
            "UPGRADED_DESCRIPTION"
            },
            race = Race.None,
            genre = Genre.None,
            cardType = CardType.Spell,
        };


        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        List<Task> asyncFunctions = new();

        parameters.target.UpdateWarriorUI();
        asyncFunctions.Add(parameters.floatingText.CreateFloatingText(parameters.target.transform, "TEXT", ColorEnum.Purple));
        await Task.WhenAll(asyncFunctions);
    }
}