using System.Text.RegularExpressions;

public class Recycle : Item {
    public Item GetItem() {
        title = GetType().Name;
        description = "When a friend dies, gain 2 shield";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override void UseOnWarriorDeath(Summoner summoner) {
        summoner.AddShield(2);
    }
}