using System.Threading.Tasks;

public class TurtleUp : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Start combat with 10 shield";
        rarity = ItemManager.Rarity.Normal;
        return this;
    }

    public override async Task UseStartOfCombat(ItemTriggerParams parameters) {
        await parameters.summoner.AddShield(10);
    }
}