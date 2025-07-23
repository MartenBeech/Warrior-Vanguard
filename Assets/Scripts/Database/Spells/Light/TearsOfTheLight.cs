using System.Collections.Generic;
using System.Threading.Tasks;

public class TearsOfTheLight {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 3 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.none,
            spellDescription = new string[] {
            "Heal all friendly warriors for 2 health",
            "Heal all friendly warriors for 3 health"
            },
            race = Character.Race.Light,
            cardType = CardType.spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        int value = cardLevel == 0 ? 2 : 3;
        List<Character> friends = gridManager.GetFriends(GameManager.turn);
        List<Task> asyncFunctions = new();
        foreach (Character friend in friends) {
            asyncFunctions.Add(friend.Heal(friend, value));
        }
        await Task.WhenAll(asyncFunctions);
    }
}