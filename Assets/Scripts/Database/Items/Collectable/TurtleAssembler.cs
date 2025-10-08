using System.Threading.Tasks;

public class TurtleAssembler : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = $"{Keyword.Initiate}: Gain 1 shield";
        rarity = ItemRarity.Normal;
        genre = Genre.None;
        return this;
    }

    public override async Task UseStartOfTurn(ItemTriggerParams parameters) {
        await parameters.summoner.AddShield(1);
    }
}