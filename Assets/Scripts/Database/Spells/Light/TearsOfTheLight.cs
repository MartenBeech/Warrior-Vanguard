using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TearsOfTheLight {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 3 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "Heal all friendly warriors for 2 health",
            "Heal all friendly warriors for 3 health"
            },
            race = Warrior.Race.Light,
            cardType = CardType.Spell,
        };
        stats.genre = (Warrior.Genre)Enum.Parse(typeof(Warrior.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        int value = parameters.cardLevel == 0 ? 2 : 3;
        List<Warrior> friends = parameters.gridManager.GetFriends(GameManager.turn);
        List<Task> asyncFunctions = new();
        foreach (Warrior friend in friends) {
            asyncFunctions.Add(friend.Heal(friend, value));
        }
        await Task.WhenAll(asyncFunctions);
    }
}