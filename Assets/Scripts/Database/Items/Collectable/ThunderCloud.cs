using System.Collections.Generic;
using System.Threading.Tasks;

public class ThunderCloud : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = $"{WarriorAbility.Keywords.Initiate}: Deal 1 magical damage to all warriors";
        rarity = ItemManager.Rarity.Normal;
        return this;
    }

    public override async Task UseStartOfTurn(ItemTriggerParams parameters) {
        GridManager gridManager = FindFirstObjectByType<GridManager>();
        List<Character> characters = gridManager.GetCharacters();
        List<Task> asyncFunctions = new();
        foreach (var character in characters) {
            asyncFunctions.Add(character.TakeDamage(character, 1, Character.DamageType.Magical));
        }
        await Task.WhenAll(asyncFunctions);
    }
}