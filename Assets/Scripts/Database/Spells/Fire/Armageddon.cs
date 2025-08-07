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
            race = Character.Race.Fire,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        List<Task> asyncFunctions = new();

        List<Character> enemies = parameters.gridManager.GetEnemies(GameManager.turn);
        foreach (Character enemy in enemies) {
            asyncFunctions.Add(enemy.TakeDamage(enemy, 5, Character.DamageType.Magical));
        }

        List<Character> friends = parameters.gridManager.GetFriends(GameManager.turn);
        foreach (Character friend in friends) {
            asyncFunctions.Add(friend.TakeDamage(friend, parameters.cardLevel == 0 ? 5 : 3, Character.DamageType.Magical));
        }

        await Task.WhenAll(asyncFunctions);
    }
}