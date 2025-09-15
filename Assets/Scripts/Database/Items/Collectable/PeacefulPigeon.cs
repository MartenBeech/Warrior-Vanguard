using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PeacefulPigeon : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = $"{Keyword.Initiate}: If the enemy summoner took no damage last turn, give your warriors +1 health";
        rarity = ItemRarity.Normal;
        return this;
    }

    public override async Task UseStartOfTurn(ItemTriggerParams parameters) {
        if (!triggeredThisTurn) {
            List<Warrior> friends = parameters.gridManager.GetFriends(parameters.summoner.stats.isFriendly ? Alignment.Friend : Alignment.Enemy);
            List<Task> asyncFunctions = new();

            foreach (var friend in friends) {
                friend.stats.AddHealth(1);
                friend.UpdateWarriorUI();
            }

            await Task.WhenAll(asyncFunctions);
        }

        triggeredThisTurn = false;
    }
}