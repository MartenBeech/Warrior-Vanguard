using System.Text.RegularExpressions;
using UnityEngine;

public class WoodenSword : Item {
    public Item GetItem() {
        title = GetType().Name;
        description = "All friendly warriors gain 1 attack";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override void UseOnWarriorSpawn(WarriorStats stats) {
        stats.AddStrength(1);
    }
}