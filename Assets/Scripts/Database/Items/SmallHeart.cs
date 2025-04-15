using System.Text.RegularExpressions;

public class SmallHeart : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All friendly warriors gain 1 health";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override void UseOnWarriorSpawn(WarriorStats stats) {
        stats.AddHealthMax(1);
    }
}