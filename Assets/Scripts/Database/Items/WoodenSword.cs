using System.Text.RegularExpressions;

public class WoodenSword : Item {
    public Item GetItem() {
        Item item = new() {
            title = GetType().Name,
            description = "All friendly warriors gain 1 attack",
        };
        item.displayTitle = Regex.Replace(item.title, "(?<!^)([A-Z])", " $1");
        return item;
    }

    public override void UseOnWarriorSpawn(WarriorStats stats) {
        for (int i = 0; i < stats.strength.Length; i++) {
            stats.strength[i] += 1;
        }
    }
}