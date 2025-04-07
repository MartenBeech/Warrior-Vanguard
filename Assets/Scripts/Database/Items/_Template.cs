using System.Text.RegularExpressions;
using UnityEngine;

public class CLASSNAME : Item {
    public Item GetItem() {
        title = GetType().Name;
        description = "DESCRIPTION";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override void UseOnWarriorSpawn(WarriorStats stats, Vector2 gridIndex) {

    }

    public override void UseImmediately() {

    }
}