using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Armageddon {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 7, 7 },
            rarity = CardRarity.Legendary,
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "Deal 5 damage to ALL warriors",
            "Deal 5 damage to all enemies and 3 damage to all friends"
            },
            race = Warrior.Race.Fire,
            cardType = CardType.Spell,
        };
        stats.genre = (Warrior.Genre)Enum.Parse(typeof(Warrior.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        List<Task> asyncFunctions = new();

        List<Warrior> enemies = parameters.gridManager.GetEnemies(GameManager.turn);
        foreach (Warrior enemy in enemies) {
            asyncFunctions.Add(enemy.TakeDamage(enemy, 5, Warrior.DamageType.Magical));
        }

        List<Warrior> friends = parameters.gridManager.GetFriends(GameManager.turn);
        foreach (Warrior friend in friends) {
            asyncFunctions.Add(friend.TakeDamage(friend, parameters.cardLevel == 0 ? 5 : 3, Warrior.DamageType.Magical));
        }

        await Task.WhenAll(asyncFunctions);
    }
}