using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FireAtWill {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 3, 3 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "Deal 2 damage to a warrior for each warrior you have",
            "Deal 3 damage to a warrior for each warrior you have"
            },
            race = Race.Fire,
            genre = Genre.Underworld,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int value = parameters.cardLevel == 0 ? 2 : 3;
        List<Warrior> friends = parameters.gridManager.GetFriends(GameManager.turn);
        int totalDamage = friends.Count * value;

        await parameters.target.TakeDamage(parameters.target, totalDamage, DamageType.Magical);
        parameters.target.UpdateWarriorUI();
    }
}