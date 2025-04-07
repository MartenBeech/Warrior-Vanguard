using System.Text.RegularExpressions;
using UnityEngine;

public class SmallHeart : Item {
    public Item GetItem() {
        title = GetType().Name;
        description = "All friendly warriors gain 1 health";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override void UseOnWarriorSpawn(WarriorStats stats, Vector2 gridIndex) {
        stats.AddHealthMax(1);
    }
}