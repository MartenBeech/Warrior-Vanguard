using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class ThunderStorm : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Deal 1 magical damage to all warriors each turn";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override async Task UseStartOfTurn(Summoner summoner) {
        GridManager gridManager = FindFirstObjectByType<GridManager>();
        List<Character> characters = gridManager.GetCharacters();
        List<Task> asyncFunctions = new();
        foreach (var character in characters) {
            asyncFunctions.Add(character.TakeDamage(character, 1, Character.DamageType.Magical));
        }
        await Task.WhenAll(asyncFunctions);
    }
}