using System.Collections.Generic;
using System.Threading.Tasks;

public class Berserk {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 6, 5 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.Warrior,
            spellDescription = new string[] {
            "A warrior strikes all other warriors",
            "A warrior strikes all other warriors"
            },
            race = Character.Race.Fire,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        List<Task> asyncFunctions = new();

        List<Character> warriors = gridManager.GetCharacters();
        warriors.Remove(target);
        foreach (Character warrior in warriors) {
            asyncFunctions.Add(target.Strike(warrior));
        }

        await Task.WhenAll(asyncFunctions);
    }
}