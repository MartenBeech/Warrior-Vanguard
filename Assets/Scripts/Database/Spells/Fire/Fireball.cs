using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Fireball {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 5, 5 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.Warrior,
            spellDescription = new string[] {
            "Deal 5 damage to a warrior and 2 damage to surrounding warriors",
            "Deal 6 damage to a warrior and 3 damage to surrounding warriors"
            },
            race = Warrior.Race.Fire,
            cardType = CardType.Spell,
        };
        stats.genre = (Warrior.Genre)Enum.Parse(typeof(Warrior.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        List<Task> asyncFunctions = new();

        int baseDamage = parameters.cardLevel == 0 ? 5 : 6;
        int surroundingDamage = parameters.cardLevel == 0 ? 2 : 3;

        await parameters.target.TakeDamage(parameters.target, baseDamage, Warrior.DamageType.Magical);

        List<Warrior> nearbyWarriors = parameters.gridManager.GetNearbyWarriors(parameters.target.gridIndex);
        foreach (Warrior warrior in nearbyWarriors) {
            asyncFunctions.Add(warrior.TakeDamage(warrior, surroundingDamage, Warrior.DamageType.Magical));
        }

        parameters.target.UpdateWarriorUI();
    }
}