using System.Text.RegularExpressions;
using UnityEngine;

public class BigHeart : Item {
    public Item GetItem() {
        title = GetType().Name;
        description = "All friendly warriors gain 2 health";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override void UseOnWarriorSpawn(WarriorStats stats) {
        stats.AddHealthMax(2);
    }
}