using System.Collections.Generic;
using System.Threading.Tasks;

public class HolyLight : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = $"{WarriorAbility.Keywords.Initiate}: Heal a random friend by 2";
        rarity = ItemManager.Rarity.Normal;
        return this;
    }

    public override async Task UseStartOfTurn(ItemTriggerParams parameters) {
        GridManager gridManager = FindFirstObjectByType<GridManager>();
        List<Character> damagedFriends = gridManager.GetDamagedFriends(GameManager.turn);

        if (damagedFriends.Count == 0) return;

        Character damagedFriend = Rng.Entry(damagedFriends);

        await damagedFriend.Heal(damagedFriend, 2);
    }
}