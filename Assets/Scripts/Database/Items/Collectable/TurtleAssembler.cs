using System.Threading.Tasks;

public class TurtleAssembler : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Gain 1 shield each turn";
        return this;
    }

    public override async Task UseStartOfTurn(Summoner summoner, Deck ownDeck, Deck enemyDeck) {
        await summoner.AddShield(1);
    }
}