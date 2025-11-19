using System;
using System.Threading.Tasks;

public class Disarm {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 3, 2 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Enemy,
            spellDescription = new string[] {
            "Set an enemy's strength to 1",
            "Set an enemy's strength to 1"
            },
            race = Race.Dark,
            genre = Genre.Undead,
            cardType = CardType.Spell,
        };


        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        parameters.target.stats.AddStrength(-(parameters.target.stats.GetStrength() - 1));
        parameters.target.UpdateWarriorUI();
        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Disarmed", ColorEnum.Purple);
    }
}