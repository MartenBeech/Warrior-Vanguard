using System.Text.RegularExpressions;

public class HumanRing : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All friendly human warriors gain 2 health";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override void UseOnFriendSpawn(WarriorStats stats) {
        if (stats.classType == Character.ClassType.Human) {
            stats.AddHealthMax(2);
        }
    }
}