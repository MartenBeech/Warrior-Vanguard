using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ExcitingBook : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Start of turn: Both summoners draw a card";
        return this;
    }

    public override async Task UseStartOfTurn(ItemTriggerParams parameters) {
        List<Task> asyncFunctions = new() {
            parameters.ownDeck.DrawCard(),
            parameters.enemyDeck.DrawCard()
        };
        await Task.WhenAll(asyncFunctions);
    }
}