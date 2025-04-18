using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class TurtleAssembler : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Gain 1 shield each turn";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override async Task UseStartOfTurn(Summoner summoner) {
        await summoner.AddShield(1);
    }
}