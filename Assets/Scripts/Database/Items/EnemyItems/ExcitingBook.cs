using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ExcitingBook : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = $"{WarriorAbility.Keywords.Initiate}: Both summoners draw a card";
        rarity = ItemManager.Rarity.None;
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