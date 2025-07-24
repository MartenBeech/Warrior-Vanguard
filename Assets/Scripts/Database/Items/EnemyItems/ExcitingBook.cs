using System.Threading.Tasks;
using UnityEngine;

public class ExcitingBook : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Both summoners draw an extra card each turn";
        return this;
    }

    public override async Task UseStartOfTurn(Summoner summoner) {
        await Task.Delay(0);
        //TODO: Make both summoners draw an extra card each turn
    }
}