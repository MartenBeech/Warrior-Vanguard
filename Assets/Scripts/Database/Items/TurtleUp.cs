using System.Text.RegularExpressions;

public class TurtleUp : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Start combat with 10 shield";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override void UseStartOfCombat(Summoner summoner) {
        summoner.AddShield(10);
    }
}