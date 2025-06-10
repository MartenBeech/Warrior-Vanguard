using System.Collections.Generic;
using System.Threading.Tasks;

public class HolyLight : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Heal a random friend by 2 each turn";
        return this;
    }

    public override async Task UseStartOfTurn(Summoner summoner) {
        GridManager gridManager = FindFirstObjectByType<GridManager>();
        List<Character> damagedFriends = gridManager.GetDamagedFriends(GameManager.turn);

        if (damagedFriends.Count == 0) return;

        Character damagedFriend = Rng.Entry(damagedFriends);

        await damagedFriend.Heal(damagedFriend, 2);
    }
}