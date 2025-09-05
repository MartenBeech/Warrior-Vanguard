using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Sanctuary {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 8, 7 },
            rarity = CardRarity.Legendary,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Make all friends immune for 1 turn",
            "Make all friends immune for 1 turn"
            },
            race = Race.Light,
            genre = Genre.Human,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        List<Warrior> friends = parameters.gridManager.GetFriends(parameters.target.stats.alignment);

        List<Task> asyncFunctions = new();
        foreach (var friend in friends) {
            friend.stats.ability.immune.Add();
            friend.UpdateWarriorUI();
            asyncFunctions.Add(parameters.floatingText.CreateFloatingText(friend.transform, "Immune", ColorEnum.Yellow));
        }
        await Task.WhenAll(asyncFunctions);
    }
}