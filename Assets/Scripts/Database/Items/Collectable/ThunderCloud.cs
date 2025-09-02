using System.Collections.Generic;
using System.Threading.Tasks;

public class ThunderCloud : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = $"{Keyword.Initiate}: Deal 1 magical damage to all warriors";
        rarity = ItemRarity.Normal;
        return this;
    }

    public override async Task UseStartOfTurn(ItemTriggerParams parameters) {
        GridManager gridManager = FindFirstObjectByType<GridManager>();
        List<Warrior> warriors = gridManager.GetWarriors();
        List<Task> asyncFunctions = new();
        foreach (var warrior in warriors) {
            asyncFunctions.Add(warrior.TakeDamage(warrior, 1, DamageType.Magical));
        }
        await Task.WhenAll(asyncFunctions);
    }
}