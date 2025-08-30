using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ForgeHammer : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = $"{WarriorAbility.Keywords.Initiate}: Give a random friend +1 strength";
        rarity = ItemManager.Rarity.Normal;
        return this;
    }

    public override async Task UseStartOfTurn(ItemTriggerParams parameters) {
        List<Character> friends = parameters.gridManager.GetFriends(parameters.summoner.stats.isFriendly ? CharacterSpawner.Alignment.Friend : CharacterSpawner.Alignment.Enemy);
        if (friends.Count == 0) return;

        Character randomFriend = Rng.Entry(friends);
        randomFriend.stats.AddStrength(1);
        randomFriend.UpdateWarriorUI();
        await parameters.floatingText.CreateFloatingText(randomFriend.transform, "+1 strength", ColorPalette.ColorEnum.Green);
    }
}