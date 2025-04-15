using System.Text.RegularExpressions;

public class TurtleAssembler : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Gain 1 shield each turn";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override void UseStartOfTurn(Summoner summoner) {
        summoner.AddShield(1);
    }
}