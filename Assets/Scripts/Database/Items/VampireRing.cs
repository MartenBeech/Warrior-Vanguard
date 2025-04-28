using System.Text.RegularExpressions;

public class VampireRing : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All friendly vampires warriors gain 2 health";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override void UseOnFriendSpawn(WarriorStats stats) {
        if (stats.race == Character.Race.Vampire) {
            stats.AddHealthMax(2);
        }
    }
}