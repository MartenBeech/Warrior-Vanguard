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
            race = Character.Race.Light,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        List<Character> friends = gridManager.GetFriends(target.stats.alignment);

        List<Task> asyncFunctions = new();
        foreach (var friend in friends) {
            friend.stats.ability.immune.Add();
            friend.UpdateWarriorUI();
            asyncFunctions.Add(floatingText.CreateFloatingText(friend.transform, "Immune", ColorPalette.ColorEnum.Yellow));
        }
        await Task.WhenAll(asyncFunctions);
    }
}