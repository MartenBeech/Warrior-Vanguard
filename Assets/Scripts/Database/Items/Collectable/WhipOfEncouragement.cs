using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WhipOfEncouragement : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = $"{Keyword.Initiate}: Deal 2 physical damage to your warriors and give them +1 strength";
        rarity = ItemRarity.Normal;
        return this;
    }

    public override async Task UseStartOfTurn(ItemTriggerParams parameters) {
        List<Warrior> friends = parameters.gridManager.GetFriends(parameters.summoner.stats.alignment);
        List<Task> asyncFunctions = new();

        foreach (var friend in friends) {
            friend.stats.AddStrength(1);
            asyncFunctions.Add(friend.TakeDamage(null, 2, DamageType.Physical));
        }

        await Task.WhenAll(asyncFunctions);
    }
}