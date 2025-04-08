using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

public class CLASSNAME : Item {
    public Item GetItem() {
        title = GetType().Name;
        description = "DESCRIPTION";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override void UseOnWarriorSpawn(WarriorStats stats) {

    }

    public override void UseImmediately() {

    }

    public override async Task UseAfterWarriorSpawn(WarriorStats stats, Vector2 gridIndex) {
        await Task.Delay(0); //This removes the CS1998 warning
    }
}