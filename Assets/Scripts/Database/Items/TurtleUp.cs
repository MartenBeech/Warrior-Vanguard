using System.Threading.Tasks;

public class TurtleUp : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Start combat with 10 shield";
        return this;
    }

    public override async Task UseStartOfCombat(Summoner summoner) {
        await summoner.AddShield(10);
    }
}