using System.Text.RegularExpressions;

public class BigHeart : Item {
    public Item GetItem() {
        Item item = new() {
            title = GetType().Name,
            description = "All friendly warriors gain 2 health",
        };
        item.displayTitle = Regex.Replace(item.title, "(?<!^)([A-Z])", " $1");
        return item;
    }

    public override void UseOnWarriorSpawn(WarriorStats stats) {
        for (int i = 0; i < stats.health.Length; i++) {
            stats.health[i] += 2;
        }
    }
}