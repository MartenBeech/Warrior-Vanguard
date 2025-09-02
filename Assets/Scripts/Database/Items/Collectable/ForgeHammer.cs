using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ForgeHammer : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = $"{Keyword.Initiate}: Give a random friend +1 strength";
        rarity = ItemRarity.Normal;
        return this;
    }

    public override async Task UseStartOfTurn(ItemTriggerParams parameters) {
        List<Warrior> friends = parameters.gridManager.GetFriends(parameters.summoner.stats.isFriendly ? Alignment.Friend : Alignment.Enemy);
        if (friends.Count == 0) return;

        Warrior randomFriend = Rng.Entry(friends);
        randomFriend.stats.AddStrength(1);
        randomFriend.UpdateWarriorUI();
        await parameters.floatingText.CreateFloatingText(randomFriend.transform, "+1 strength", ColorPalette.ColorEnum.Green);
    }
}