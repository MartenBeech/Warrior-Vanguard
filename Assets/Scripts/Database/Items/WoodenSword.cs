using System.Text.RegularExpressions;

public class WoodenSword : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All friendly warriors gain 1 attack";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override void UseOnFriendSpawn(WarriorStats stats) {
        stats.AddStrength(1);
    }
}