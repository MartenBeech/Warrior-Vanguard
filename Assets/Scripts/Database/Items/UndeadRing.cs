using System.Text.RegularExpressions;

public class UndeadRing : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All friendly undead warriors gain 2 health";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override void UseOnFriendSpawn(WarriorStats stats) {
        if (stats.classType == Character.ClassType.Undead) {
            stats.AddHealthMax(2);
        }
    }
}