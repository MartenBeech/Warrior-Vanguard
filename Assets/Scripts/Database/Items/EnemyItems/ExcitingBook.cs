using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ExcitingBook : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Start of turn: Both summoners draw a card";
        return this;
    }

    public override async Task UseStartOfTurn(Summoner summoner, Deck ownDeck, Deck enemyDeck, Hand enemyHand) {
        List<Task> asyncFunctions = new() {
            ownDeck.DrawCard(),
            enemyDeck.DrawCard()
        };
        await Task.WhenAll(asyncFunctions);
    }
}