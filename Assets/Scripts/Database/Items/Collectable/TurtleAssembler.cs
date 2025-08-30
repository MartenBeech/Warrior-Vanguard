using System.Threading.Tasks;

public class TurtleAssembler : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = $"{WarriorAbility.Keywords.Initiate}: Gain 1 shield";
        return this;
    }

    public override async Task UseStartOfTurn(ItemTriggerParams parameters) {
        await parameters.summoner.AddShield(1);
    }
}