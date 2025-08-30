using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CharmOfPeasants : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = $"{WarriorAbility.Keywords.Initiate}: Draw an extra card";
        return this;
    }

    public override async Task UseStartOfTurn(ItemTriggerParams parameters) {
        List<Task> asyncFunctions = new() {
            parameters.ownDeck.DrawCard(),
        };
        await Task.WhenAll(asyncFunctions);
    }
}