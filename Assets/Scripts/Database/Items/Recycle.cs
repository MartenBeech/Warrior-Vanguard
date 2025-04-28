using System.Threading.Tasks;

public class Recycle : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When a friend dies, gain 2 shield";
        return this;
    }

    public override async Task UseOnWarriorDeath(Summoner summoner) {
        await summoner.AddShield(2);
    }
}